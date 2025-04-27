using SmartClinic.Application.Features.Profile.Query;

namespace SmartClinic.Application.Features.Profile.Mapper
{
    public class UserProfileMapper
    {
        public static BaseProfile Map(AppUser user)
        {
            return new BaseProfile
            {
                Email = user.Email!,
                FirstName = user.FirstName!,
                LastName = user.LastName!,
                PhoneNumber = user.PhoneNumber!,
                Address = user.Address!,
                ProfileImage = user.ProfileImage!,
            };
        }
    }
}
