namespace SmartClinic.Application.Features.Auth;

public class AuthService(
    IConfiguration configuration,
    UserManager<AppUser> userMGR,
    SignInManager<AppUser> signMGR,
    ResponseHandler response,
    IFileHandlerService fileHandler) : IAuthService
{
    private readonly IConfiguration configuration = configuration;
    private readonly UserManager<AppUser> userMGR = userMGR;
    private readonly SignInManager<AppUser> signMGR = signMGR;
    private readonly ResponseHandler response = response;
    private readonly IFileHandlerService fileHandler = fileHandler;

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
            new(JwtRegisteredClaimNames.Email, user.Email!),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()) ,
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
            return response.BadRequest<LoginResponseDTO>("invalid login attempts")!;

        var sign = await signMGR.CheckPasswordSignInAsync(userExist, user.Password, false);

        if (!sign.Succeeded)
            return response.BadRequest<LoginResponseDTO>("invalid login attempts")!;

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
            fileResult = await fileHandler.HandleFile(newPatientUser.Image, validationsOptions);
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
            LastName = newPatientUser.Lastname,
            Address = newPatientUser.Address,
            ProfileImage = fileResult.RelativeFilePath,
            Patient = new Patient(),
        };
        //////////////////

        var result = await userMGR.CreateAsync(user, newPatientUser.Password);

        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => e.Description).ToList();
            return response.BadRequest<RegisterResponseDTO>(errors: errors)!;
        }
        /////////////////
        var role = await userMGR.AddToRoleAsync(user, "patient");

        if (!role.Succeeded)
        {
            var errors = role.Errors.Select(e => e.Description).ToList();
            return response.BadRequest<RegisterResponseDTO>(errors: errors)!;
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


    public async Task<Response<ImgResponse>> GetProfileImg(string id)
    {
        var user = await userMGR.FindByIdAsync(id);

        if (user == null)
        {
            return response.BadRequest<ImgResponse>("invalid login attempts")!;
        }

        return response.Success(new ImgResponse()
        {
            profileImg = fileHandler.GetFileURL(user.ProfileImage!)
        }, message: "success");

    }


}
