using System.Security.Claims;
using Models.DTOs.Auth;
using SmartClinic.Application.Bases;
using SmartClinic.Application.Features.Auth;
using SmartClinic.Application.Features.Auth.Command;
using SmartClinic.Application.Features.Profile.Command;
using SmartClinic.Application.Features.Profile.Query;
using SmartClinic.Domain.DTOs.Auth;

namespace SmartClinic.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : AppControllerBase
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
    [ProducesResponseType<Response<LoginResponseDTO>>(StatusCodes.Status200OK)]
    [ProducesResponseType<Response<LoginResponseDTO>>(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType<Response<LoginResponseDTO>>(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Login([FromBody] LoginRequestDTO user)
    {
        Response<LoginResponseDTO> res = await authService.Login(user);

        return NewResult(res);
    }


    [HttpPost("register")]
    [Consumes("multipart/form-data")]
    [ProducesResponseType<Response<RegisterResponseDTO>>(StatusCodes.Status201Created)]
    [ProducesResponseType<Response<RegisterResponseDTO>>(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromForm] RegisterRequestDTO newPatientUser)
    {
        var res = await authService.Register(newPatientUser);

        return NewResult(res);
    }

    [HttpGet("GetProfileImg")]
    [Authorize]
    [ProducesResponseType<Response<ImgResponse>>(StatusCodes.Status200OK)]
    [ProducesResponseType<Response<ImgResponse>>(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetProfileImg()
    {
        var id = User.FindAll(ClaimTypes.NameIdentifier).FirstOrDefault()?.Value;
        var result = await authService.GetProfileImg(id!);

        return NewResult(result);
    }

    [HttpGet("profile")]
    [Authorize]
    [ProducesResponseType<Response<ProfileResponse>>(StatusCodes.Status200OK)]
    [ProducesResponseType<Response<ProfileResponse>>(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetProfile()
    {
        var id = User.FindAll(ClaimTypes.NameIdentifier).FirstOrDefault()?.Value;

        var res = await profileService.GetProfile(id!);
        return NewResult(res);
    }

    [HttpDelete("remove_profileImg")]
    [Authorize]
    [ProducesResponseType<Response<ProfileResponse>>(StatusCodes.Status200OK)]
    [ProducesResponseType<Response<ProfileResponse>>(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> RemoveProfileImg()
    {
        var id = User.FindAll(ClaimTypes.NameIdentifier).FirstOrDefault()?.Value;

        var res = await profileService.RemoveImg(id!);
        return NewResult(res);
    }

    [HttpPost("Update_ProfileImg")]
    [Authorize]
    [ProducesResponseType<Response<ImgResponse>>(StatusCodes.Status200OK)]
    [ProducesResponseType<Response<ImgResponse>>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> UpdateProfile([FromForm] ImgUpdateRequest file)
    {
        var id = User.FindAll(ClaimTypes.NameIdentifier).FirstOrDefault()?.Value;

        var res = await profileService.UpdateProfileImg(file, id);
        return NewResult(res);
    }

}
