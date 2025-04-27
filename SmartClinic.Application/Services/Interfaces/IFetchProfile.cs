using SmartClinic.Application.Features.Profile.Query;

namespace SmartClinic.Application.Services.Interfaces
{
    public interface IFetchProfile
    {
        string Role { get; set; }
        Task<BaseProfile> FetchAsync(AppUser user);

    }
}
