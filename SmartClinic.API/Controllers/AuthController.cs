using Microsoft.AspNetCore.Mvc;
using Models.DTOs.Auth;
using SmartClinic.Application.Bases;
using SmartClinic.Application.Features.Auth;

namespace SmartClinic.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ResponseHandler response;
        private readonly IAuthService authService;

        public AuthController(ResponseHandler _response, IAuthService _authService)
        {
            response = _response;
            authService = _authService;
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

    }
}
