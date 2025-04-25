using Models.DTOs.Auth;
using SmartClinic.Application.Bases;
using SmartClinic.Domain.DTOs.Auth;

namespace SmartClinic.Application.Features.Auth
{
    public interface IAuthService
    {
        Task<Response<LoginResponseDTO>> Login(LoginRequestDTO user);

        Task<Response<RegisterResponseDTO>> Register(RegisterRequestDTO newPatientUser);

    }
}
