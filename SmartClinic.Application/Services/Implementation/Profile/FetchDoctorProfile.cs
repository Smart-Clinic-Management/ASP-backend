using SmartClinic.Application.Features.Profile.Command;

namespace SmartClinic.Application.Services.Implementation.Profile;

public class FetchDoctorProfile : IFetchProfile
{
    private readonly IFileHandlerService fileHandler;

    public string Role { get; set; } = "Doctor";

    public FetchDoctorProfile(IFileHandlerService fileHandler)
    {
        this.fileHandler = fileHandler;
    }

    public async Task<BaseProfile> FetchAsync(AppUser user)
    {

        var img = fileHandler.GetFileURL(user.ProfileImage!);

        return new DoctorProfile
        {
            Email = user.Email!,
            Address = user.Address!,
            FirstName = user.FirstName!,
            LastName = user.LastName!,
            PhoneNumber = user.PhoneNumber!,
            ProfileImage = img!,
            Specialization = user.Doctor?.Specialization!,
        };

    }
}
