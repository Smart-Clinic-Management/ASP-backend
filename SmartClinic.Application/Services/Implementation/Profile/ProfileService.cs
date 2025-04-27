using SmartClinic.Application.Features.Profile.Query;

namespace SmartClinic.Application.Services.Implementation.Profile
{
    internal class ProfileService : IProfileService
    {
        private readonly UserManager<AppUser> userMGR;
        private readonly ResponseHandler response;
        private readonly Dictionary<string, IFetchProfile> profiles;

        public ProfileService(UserManager<AppUser> userMGR, ResponseHandler response, IEnumerable<IFetchProfile> profiles)
        {
            this.userMGR = userMGR;
            this.response = response;
            this.profiles = profiles.ToDictionary(p => p.Role.ToLowerInvariant());
        }


        public async Task<Response<ProfileResponse>> GetProfile(string userId)
        {
            var user = await userMGR.FindByEmailAsync(userId);

            if (user == null)
            {
                return response.NotFound<ProfileResponse>();
            }

            var role = await userMGR.GetRolesAsync(user);


            profiles.TryGetValue(role.FirstOrDefault()!, out var fetcher);

            var result = new ProfileResponse
            {
                details = await fetcher!.FetchAsync(user)
            };

            return response.Success<ProfileResponse>(result);
        }
    }
}
