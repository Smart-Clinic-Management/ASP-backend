using Models.DTOs.Auth;
using SmartClinic.Application.Bases;

namespace SmartClinic.Application.Features.Auth
{
    public interface IAuthService
    {
        Task<Response<LoginResponseDTO>> Login(LoginRequestDTO user);

    }
}
