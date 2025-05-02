using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models.DTOs.Auth;
using SmartClinic.Application.Features.Auth.Command;
using SmartClinic.Domain.DTOs.Auth;

namespace SmartClinic.Application.Features.Auth;

public class AuthService : IAuthService
{
    private readonly IConfiguration configuration;
    private readonly UserManager<AppUser> userMGR;
    private readonly SignInManager<AppUser> signMGR;
    private readonly ResponseHandler response;
    private readonly IUnitOfWork uof;
    private readonly IFileHandlerService fileHandler;
    private readonly IHttpContextAccessor httpContext;

    public AuthService(IConfiguration configuration, UserManager<AppUser> userMGR, SignInManager<AppUser> signMGR, ResponseHandler response, IUnitOfWork uof, IFileHandlerService fileHandler, IHttpContextAccessor request)
    {
        this.configuration = configuration;
        this.userMGR = userMGR;
        this.signMGR = signMGR;
        this.response = response;
        this.uof = uof;
        this.fileHandler = fileHandler;
        this.httpContext = request;
    }

    public async Task<string> GenerateJWT(AppUser user)
    {

        var userRoles = await userMGR.GetRolesAsync(user);


        var q = await userMGR.Users
            .Include(u => u.Patient)
            .Include(u => u.Doctor)
            .Where(u => u.Id == user.Id)
            .Select(u => new
            {
                Id = (int?)u.Patient.Id ?? u.Doctor.Id
            }).FirstOrDefaultAsync();


        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Email!),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(ClaimTypes.NameIdentifier, user.Id.ToString()) ,
            new("id" , q?.Id.ToString() ?? "") ,
        };

        claims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));



        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["ApiSettings:Secret"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(

            issuer: configuration["ApiSettings:Issuer"],
            audience: configuration["ApiSettings:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddDays(7),
            signingCredentials: creds);


        return new JwtSecurityTokenHandler().WriteToken(token);
    }


    public async Task<Response<LoginResponseDTO>> Login(LoginRequestDTO user)
    {


        var userExist = await userMGR.FindByEmailAsync(user.Email);
        if (userExist == null)
            return response.BadRequest<LoginResponseDTO>(["invalid login attemps"])!;

        var sign = await signMGR.CheckPasswordSignInAsync(userExist, user.Password, false);

        if (!sign.Succeeded)
            return response.BadRequest<LoginResponseDTO>(["invalid login attemps"])!;

        var res = response.Success(new LoginResponseDTO() { Token = await GenerateJWT(userExist) }, message: "success");

        return res!;
    }

    public async Task<Response<RegisterResponseDTO>> Register(RegisterRequestDTO newPatientUser)
    {
        /////////////////////

        var fileResult = new FileValidationResult();
        if (newPatientUser.Image != null)
        {
            var validationsOptions = new FileValidation
            {
                MaxSize = 2 * 1024 * 1024,
                AllowedExtenstions = [".jpg", ".jpeg", ".png"]
            };
            fileResult = await fileHandler.HanldeFile(newPatientUser.Image, validationsOptions);
            if (!fileResult.Success)
            {
                return response.BadRequest<RegisterResponseDTO>(errors: [fileResult.Error]);
            }
        }

        ////////////////////

        var user = new AppUser()
        {
            UserName = newPatientUser.Email,
            Email = newPatientUser.Email,
            FirstName = newPatientUser.Firstname,
            Address = newPatientUser.Address,
            ProfileImage = fileResult.RelativeFilePath,
            Patient = new Patient(),
        };
        //////////////////

        var result = await userMGR.CreateAsync(user, newPatientUser.Password);

        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => e.Description).ToList();
            return response.BadRequest<RegisterResponseDTO>(errors)!;
        }
        /////////////////
        var role = await userMGR.AddToRoleAsync(user, "patient");

        if (!role.Succeeded)
        {
            var errors = role.Errors.Select(e => e.Description).ToList();
            return response.BadRequest<RegisterResponseDTO>(errors)!;
        }
        if (fileResult.Success)
        {
            await fileHandler.SaveFile(newPatientUser.Image!, fileResult.FullFilePath!);
        }
        ////////////////

        var res = response.Created(new RegisterResponseDTO()
        {
            Email = user.Email,
            Id = user.Id
        });

        return res!;
    }


    public async Task<Response<ImgResponse>> GetProfileImg(string email)
    {
        var user = await userMGR.FindByEmailAsync(email);

        if (user == null)
        {
            return response.BadRequest<ImgResponse>(["invalid login attemps"])!;
        }

        return response.Success(new ImgResponse()
        {
            profileImg = fileHandler.GetFileURL(user.ProfileImage!)
        }, message: "success");

    }


}
