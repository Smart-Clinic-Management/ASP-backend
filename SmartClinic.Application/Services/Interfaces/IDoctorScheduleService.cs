using SmartClinic.Application.Features.DoctorsSchedules.Command.CreateDoctorSchedule;
using SmartClinic.Application.Features.DoctorsSchedules.Query.DTOs.GetDoctorSchedule;

namespace SmartClinic.Application.Services.Interfaces;

public interface IDoctorScheduleService
{
    //Task<Response<IEnumerable<GetDoctorSchedule>>> GetSchedulesForDoctorAsync(int doctorId);

    Task<Response<string>> DeleteScheduleAsync(DeleteDoctorScheduleRequest deleteDoctorSchedule);

    Task<Response<GetDoctorSchedule>> CreateAsync(CreateDoctorScheduleRequest request);

}
