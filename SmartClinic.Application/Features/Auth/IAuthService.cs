namespace SmartClinic.Application.Features.Auth;

public interface IAuthService
{
    Task<Response<LoginResponseDTO>> Login(LoginRequestDTO user);

    Task<Response<RegisterResponseDTO>> Register(RegisterRequestDTO newPatientUser);

    Task<Response<ImgResponse>> GetProfileImg(string id);


}
