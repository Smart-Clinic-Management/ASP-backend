using SmartClinic.Application.Features.Profile.Query;

namespace SmartClinic.Application.Services.Interfaces
{
    public interface IProfileService
    {

        Task<Response<ProfileResponse>> GetProfile(string userId);


    }
}
