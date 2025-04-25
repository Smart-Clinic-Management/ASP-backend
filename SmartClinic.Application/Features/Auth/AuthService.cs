using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models.DTOs.Auth;
using SmartClinic.Application.Bases;
using SmartClinic.Domain.DTOs.Auth;
using SmartClinic.Domain.Entities;

namespace SmartClinic.Application.Features.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration configuration;
        private readonly UserManager<AppUser> userMGR;
        private readonly SignInManager<AppUser> signMGR;
        private readonly ResponseHandler response;

        public AuthService(IConfiguration configuration, UserManager<AppUser> userMGR, SignInManager<AppUser> signMGR, ResponseHandler response)
        {
            this.configuration = configuration;
            this.userMGR = userMGR;
            this.signMGR = signMGR;
            this.response = response;
        }

        public async Task<string> GenerateJWT(AppUser user)
        {

            var userRoles = await userMGR.GetRolesAsync(user);


            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id) ,
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


            var res = response.Success(new LoginResponseDTO() { Token = await GenerateJWT(userExist) });

            return res!;
        }

        public async Task<Response<RegisterResponseDTO>> Register(RegisterRequestDTO newPatientUser)
        {
            var user = new AppUser()
            {
                UserName = newPatientUser.Email,
                Email = newPatientUser.Email,
                FirstName = newPatientUser.Firstname,
                Address = newPatientUser.Address,
            };

            var result = await userMGR.CreateAsync(user, newPatientUser.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                return response.BadRequest<RegisterResponseDTO>(errors)!;
            }

            var role = await userMGR.AddToRoleAsync(user, "patient");

            if (!role.Succeeded)
            {
                var errors = role.Errors.Select(e => e.Description).ToList();
                return response.BadRequest<RegisterResponseDTO>(errors)!;
            }

            var res = response.Success(new RegisterResponseDTO()
            {
                Email = user.Email,
                Id = user.Id
            });

            return res!;
        }
    }
}
