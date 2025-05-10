namespace SmartClinic.Application.Services.Interfaces;

public interface IDoctorScheduleService
{
    Task<Response<string>> DeleteScheduleAsync(DeleteDoctorScheduleRequest deleteDoctorSchedule);

    Task<Response<GetDoctorSchedule>> CreateAsync(CreateDoctorScheduleRequest request);

}
