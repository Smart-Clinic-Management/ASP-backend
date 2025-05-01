using SmartClinic.Application.Features.Doctors.Query.DTOs.GetDoctorWithAvailableAppointment;

namespace SmartClinic.Application.Features.Doctors.Mapper;

public static class DoctorMappingExtensions
{

    public static GetDoctorWithAvailableAppointment ToGetDoctorWithAvailableAppointment(this Doctor doctor, List<AvailableSchedule> AvailableSchedules)
    {
        return new GetDoctorWithAvailableAppointment
        (
             doctor.User.FirstName,
             doctor.User.LastName,
             doctor.User.Email!,
             doctor.User.PhoneNumber!,
             doctor.User.Age,
            doctor.User.Address,
             doctor.Description,
            doctor.WaitingTime,
             doctor.User.ProfileImage!,
             [.. doctor.Specializations.Select(x => x.Name)],
             AvailableSchedules
        );
    }

    public static GetDoctorByIdResponse ToGetDoctorByIdResponse(this Doctor doctor)
    {
        return new GetDoctorByIdResponse(
            firstName: doctor.User.FirstName,
            lastName: doctor.User.LastName,
            userEmail: doctor.User.Email,
            userPhoneNumber: doctor.User.PhoneNumber,
            age: doctor.User.Age,
            address: doctor.User.Address,
            description: doctor.Description,
            waitingTime: doctor.WaitingTime,
            image: doctor.User.ProfileImage,
            SlotDuration: doctor.DoctorSchedules.FirstOrDefault()?.SlotDuration,
            Specializations: doctor.Specializations.Select(s => s.Name).ToList()
        );
    }


    public static GetAllDoctorsResponse ToGetAllDoctorsResponse(this Doctor doctor)
    {
        return new GetAllDoctorsResponse(
                Id: doctor.Id,
            firstName: doctor.User.FirstName,
            lastName: doctor.User.LastName,
            image: doctor.User.ProfileImage,
            Specializations: doctor.Specializations.Select(s => s.Name).ToList()
        );
    }

    public static UpdateDoctorResponse ToUpdateDoctorResponse(this Doctor doctor, IHttpContextAccessor _httpContextAccessor)
    {
        return new UpdateDoctorResponse(
            Fname: doctor.User.FirstName,
            Lname: doctor.User.LastName,
            Email: doctor.User.Email,
            Image: doctor.User.ProfileImage != null ? GetImgUrl(doctor.User.ProfileImage, _httpContextAccessor) : null,
            Specialization: doctor.Specializations.Select(s => s.Id).ToList(),
            BirthDate: doctor.User.BirthDate,
            Address: doctor.User.Address,
            WaitingTime: doctor.WaitingTime,
            Description: doctor.Description
        );
    }
    public static string GetImgUrl(string? path, IHttpContextAccessor _httpContextAccessor)
    {
        if (path == null) return null!;

        var request = _httpContextAccessor.HttpContext?.Request;
        return $"{request!.Scheme}://{request.Host}/{path.Replace("\\", "/")}";
    }

    public static async Task<FileValidationResult> ValidateAndSaveFileAsync(IFormFile file, FileValidation validationOptions, IFileHandlerService fileHandler)
    {
        var fileResult = await fileHandler.HanldeFile(file, validationOptions);
        if (!fileResult.Success)
        {
            return fileResult;
        }

        await fileHandler.SaveFile(file, fileResult.FullFilePath);
        return fileResult;
    }

    public static async Task AddSpecializationsToDoctorAsync(this Doctor doctor, ISpecializaionRepository specializationRepo, List<int> specializationIds)
    {
        if (specializationIds != null && specializationIds.Any())
        {
            foreach (var specializationId in specializationIds)
            {
                var specialization = await specializationRepo.GetByIdAsync(specializationId);
                if (specialization != null)
                {
                    doctor.Specializations.Add(specialization); // هنا بيضيف الكيان مش DTO
                }
            }
        }
    }
}
