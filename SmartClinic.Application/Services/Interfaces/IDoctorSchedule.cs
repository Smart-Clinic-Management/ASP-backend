using SmartClinic.Application.Features.DoctorSchedule.Command.DeleteDoctorSchedule;
using SmartClinic.Application.Features.Patients.Query.DTOs.GetDoctorSchedule;

namespace SmartClinic.Application.Services.Interfaces
{
    public interface IDoctorScheduleService
    {
        Task<Response<IEnumerable<GetDoctorSchedule>>> GetSchedulesForDoctorAsync(int doctorId);

        Task<DeleteSchedulesResponse> DeleteScheduleByIdAsync(int scheduleId);
    }
}
