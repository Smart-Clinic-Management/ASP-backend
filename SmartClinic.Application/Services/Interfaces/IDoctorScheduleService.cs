using SmartClinic.Application.Features.DoctorsSchedules.Command.DeleteDoctorSchedule;

namespace SmartClinic.Application.Services.Interfaces;

public interface IDoctorScheduleService
{
    //Task<Response<IEnumerable<GetDoctorSchedule>>> GetSchedulesForDoctorAsync(int doctorId);

    Task<Response<string>> DeleteScheduleAsync(DeleteDoctorScheduleRequest deleteDoctorSchedule);

    //Task<Response<GetDoctorSchedule>> CreateAsync(CreateDoctorScheduleRequest request);

    //Task<Response<GetDoctorSchedule>> UpdateAsync(UpdateDoctorScheduleRequest request);
}
