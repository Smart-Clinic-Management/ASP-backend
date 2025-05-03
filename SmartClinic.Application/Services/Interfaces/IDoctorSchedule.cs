using SmartClinic.Application.Features.DoctorSchedule.Command.CreateDoctorSchedule;
using SmartClinic.Application.Features.DoctorSchedule.Command.DeleteDoctorSchedule;
using SmartClinic.Application.Features.DoctorSchedule.Command.UpdateDoctorSchedule;
using SmartClinic.Application.Features.DoctorSchedule.Query.DTOs.GetDoctorSchedule;

namespace SmartClinic.Application.Services.Interfaces;

public interface IDoctorScheduleService
{
    Task<Response<IEnumerable<GetDoctorSchedule>>> GetSchedulesForDoctorAsync(int doctorId);

    Task<Response<DeleteSchedulesResponse>> DeleteScheduleByIdAsync(int scheduleId);

    Task<Response<GetDoctorSchedule>> CreateAsync(CreateDoctorScheduleRequest request);

    Task<Response<GetDoctorSchedule>> UpdateAsync(UpdateDoctorScheduleRequest request);
}
