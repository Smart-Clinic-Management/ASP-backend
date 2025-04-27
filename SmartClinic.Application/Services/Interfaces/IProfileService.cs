using SmartClinic.Application.Features.Auth.Command;
using SmartClinic.Application.Features.Profile.Command;
using SmartClinic.Application.Features.Profile.Query;

namespace SmartClinic.Application.Services.Interfaces
{
    public interface IProfileService
    {

        Task<Response<ProfileResponse>> GetProfile(string userId);

        Task<Response<ProfileResponse>> RemoveImg(string userid);

        Task<Response<ImgResponse>> UpdateProfileImg(ImgUpdateRequest request, string userid);

    }
}
