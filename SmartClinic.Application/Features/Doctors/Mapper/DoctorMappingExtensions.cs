using SmartClinic.Application.Features.Doctors.Command.CreateDoctor;
using SmartClinic.Application.Features.Doctors.Command.UpdateDoctor;
using SmartClinic.Application.Features.Doctors.Query.GetDoctors;
using SmartClinic.Application.Features.Doctors.Query.GetDoctorWithAvailableAppointment;

namespace SmartClinic.Application.Features.Doctors.Mapper;

public static class DoctorMappingExtensions
{

    public static GetDoctorWithAvailableAppointment ToGetDoctorWithAvailableSchedules(this Doctor doctor, List<AvailableSchedule> AvailableSchedules)
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
             doctor.Specialization.Id,
             doctor.Specialization.Name,
             AvailableSchedules
        );
    }

    public static AppUser ToUser(this CreateDoctorRequest newDoctorUser) => new()
    {
        Address = newDoctorUser.Address,
        UserName = newDoctorUser.Email,
        FirstName = newDoctorUser.FirstName,
        LastName = newDoctorUser.LastName,
        Email = newDoctorUser.Email,
        BirthDate = newDoctorUser.BirthDate,
        PhoneNumber = newDoctorUser.PhoneNumber,
        ProfileImage = newDoctorUser.Image.ToRelativeFilePath(),
        Doctor = new Doctor()
        {
            Description = newDoctorUser.Description,
            WaitingTime = newDoctorUser.WaitingTime,
            SpecializationId = newDoctorUser.SpecializationId,
        }
    };

    //public static GetDoctorByIdResponse ToGetDoctorByIdResponse(this Doctor doctor)
    //{
    //    return new GetDoctorByIdResponse(
    //        FirstName: doctor.User.FirstName,
    //        LastName: doctor.User.LastName,
    //        UserEmail: doctor.User.Email,
    //        PhoneNumber: doctor.User.PhoneNumber,
    //        Age: doctor.User.Age,
    //        Address: doctor.User.Address,
    //        Description: doctor.Description,
    //        WaitingTime: doctor.WaitingTime,
    //        Image: doctor.User.ProfileImage,
    //        Specialization: doctor.Specialization.Name
    //    );
    //}


    public static GetAllDoctorsResponse ToGetAllDoctorsResponse(this Doctor doctor)
    {
        return new GetAllDoctorsResponse
        {
            Id = doctor.Id,
            FirstName = doctor.User.FirstName,
            Age = doctor.User.Age,
            Specialization = doctor.Specialization.Name,
            Image = doctor.User.ProfileImage,
            LastName = doctor.User.LastName

        };
    }

    //public static UpdateDoctorResponse ToUpdateDoctorResponse(this Doctor doctor, IHttpContextAccessor _httpContextAccessor)
    //{
    //    return new UpdateDoctorResponse(
    //        Fname: doctor.User.FirstName,
    //        Lname: doctor.User.LastName,
    //        Email: doctor.User.Email,
    //        Image: doctor.User.ProfileImage != null ? GetImgUrl(doctor.User.ProfileImage, _httpContextAccessor) : null,
    //        SpecializationId: doctor.Specialization.Id,
    //        BirthDate: doctor.User.BirthDate,
    //        Address: doctor.User.Address,
    //        WaitingTime: doctor.WaitingTime,
    //        Description: doctor.Description
    //    );
    //}

    public static UpdateDoctorResponse ToUpdateDto(this Doctor doctor, IFileHandlerService fileHandler) => new()
    {
        Age = doctor.User.Age,
        Image = fileHandler.GetFileURL(doctor.User.ProfileImage!),
        LastName = doctor.User.LastName,
        BirthDate = doctor.User.BirthDate,
        Description = doctor.Description,
        FirstName = doctor.User.FirstName,
        WaitingTime = doctor.WaitingTime

    };


    public static Doctor UpdateEntity(this Doctor doctor, UpdateDoctorRequest request)
    {
        doctor.User.FirstName = request.Fname ?? doctor.User.FirstName;
        doctor.User.LastName = request.Lname ?? doctor.User.LastName;
        doctor.User.BirthDate = request.BirthDate ?? doctor.User.BirthDate;
        doctor.User.ProfileImage = request.Image?.ToRelativeFilePath() ?? doctor.User.ProfileImage;
        doctor.User.Address = request.Address ?? doctor.User.Address;
        doctor.WaitingTime = request.WaitingTime ?? doctor.WaitingTime;
        doctor.User.PhoneNumber = request.PhoneNumber ?? doctor.User.PhoneNumber;
        doctor.Description = request.Description ?? doctor.Description;

        return doctor;
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

    //public static async Task AddSpecializationsToDoctorAsync(this Doctor doctor, ISpecializationRepository specializationRepo, int? specializationId)
    //{
    //    if (!specializationId.HasValue) return;

    //    var specialization = await specializationRepo.GetByIdAsync(specializationId.Value);
    //    if (specialization != null)
    //    {
    //        doctor.Specialization = specialization;
    //    }
    //}

}

