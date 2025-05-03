using SmartClinic.Application.Features.Profile.Command;

namespace SmartClinic.Application.Services.Implementation.Profile
{
    class FetchPatientProfile : IFetchProfile
    {
        private readonly IFileHandlerService fileHandler;

        public string Role { get; set; } = "Patient";

        public FetchPatientProfile(IFileHandlerService fileHandler)
        {
            this.fileHandler = fileHandler;
        }

        public async Task<BaseProfile> FetchAsync(AppUser user)
        {


            var img = fileHandler.GetFileURL(user.ProfileImage!);


            return new PatientProfile
            {
                Id = user.Id,
                Email = user.Email!,
                Address = user.Address!,
                FirstName = user.FirstName!,
                LastName = user.LastName!,
                PhoneNumber = user.PhoneNumber!,
                ProfileImage = img!,
                MedicalHistory = user.Patient?.MedicalHistory!,
            };
        }
    }
}
