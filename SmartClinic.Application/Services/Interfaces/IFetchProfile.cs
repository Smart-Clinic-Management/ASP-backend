using SmartClinic.Application.Features.Profile.Command;

namespace SmartClinic.Application.Services.Interfaces
{
    public interface IFetchProfile
    {
        string Role { get; set; }
        Task<BaseProfile> FetchAsync(AppUser user);

    }
}
