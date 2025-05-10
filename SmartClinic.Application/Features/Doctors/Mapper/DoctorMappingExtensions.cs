namespace SmartClinic.Application.Features.Doctors.Mapper;

public static class DoctorMappingExtensions
{

    public static Doctor Delete(this Doctor doctor)
    {
        doctor.IsActive = false;
        doctor.User.IsActive = false;
        doctor.User.Email = null;


        foreach (var appointment in doctor.Appointments)
            appointment.Status = AppointmentStatus.Canceled;

        return doctor;
    }

    public static GetDoctorWithSchedulesSlotsResponse ToGetDoctorWithAvailableSchedules(this Doctor doctor, List<AvailableSchedule> AvailableSchedules)
    {
        return new GetDoctorWithSchedulesSlotsResponse
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


}

