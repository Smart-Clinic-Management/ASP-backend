using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs.Auth;
using SmartClinic.Application.Bases;
using SmartClinic.Application.Features.Auth;
using SmartClinic.Application.Features.Profile.Command;
using SmartClinic.Application.Services.Interfaces;

namespace SmartClinic.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ResponseHandler response;
        private readonly IAuthService authService;
        private readonly IProfileService profileService;

        public AuthController(ResponseHandler _response, IAuthService _authService, IProfileService profileService)
        {
            response = _response;
            authService = _authService;
            this.profileService = profileService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO user)
        {
            Response<LoginResponseDTO> res = await authService.Login(user);

            return Ok(res);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] RegisterRequestDTO newPatientUser)
        {
            var res = await authService.Register(newPatientUser);

            return Ok(res);
        }

        [HttpGet("GetProfileImg")]
        [Authorize]
        public async Task<IActionResult> GetProfileImg()
        {
            var id = User.FindAll(ClaimTypes.NameIdentifier).FirstOrDefault()?.Value;
            var result = await authService.GetProfileImg(id!);

            return Ok(result);
        }

        [HttpGet("profile")]
        [Authorize]
        public async Task<IActionResult> GetProfile()
        {
            var id = User.FindAll(ClaimTypes.NameIdentifier).FirstOrDefault()?.Value;

            var res = await profileService.GetProfile(id!);
            return Ok(res);
        }

        [HttpDelete("remove_profileImg")]
        [Authorize]
        public async Task<IActionResult> RemoveProfileImg()
        {
            var id = User.FindAll(ClaimTypes.NameIdentifier).FirstOrDefault()?.Value;

            var res = await profileService.RemoveImg(id!);
            return Ok(res);
        }

        [HttpPost("Update_Profile")]
        [Authorize]
        public async Task<IActionResult> UpdateProfile([FromForm] ImgUpdateRequest file)
        {
            var id = User.FindAll(ClaimTypes.NameIdentifier).FirstOrDefault()?.Value;

            var res = await profileService.UpdateProfileImg(file, id);
            return Ok(res);
        }

    }
}
