using SmartClinic.Application.Features.Patients.Query.DTOs.GetDoctorSchedule;
using SmartClinic.Infrastructure.Repos;
using SmartClinic.Application.Services.Interfaces;
using SmartClinic.Infrastructure.Interfaces;
using SmartClinic.Application.MappingExtensions;
using SmartClinic.Application.Features.Patients.Query.DTOs.GetDoctorSchedule;
using SmartClinic.Application.Features.DoctorSchedule.Command.DeleteDoctorSchedule;

namespace SmartClinic.Application.Services.Implementation
{
    public class DoctorScheduleServices : IDoctorScheduleService
    {
        private readonly IDoctorSchedule _doctorScheduleRepo;

        public DoctorScheduleServices(IDoctorSchedule doctorScheduleRepo)
        {
            _doctorScheduleRepo = doctorScheduleRepo;
        }

        public async Task<Response<IEnumerable<GetDoctorSchedule>>> GetSchedulesForDoctorAsync(int doctorId)
        {
            var schedules = await _doctorScheduleRepo.GetByDoctorIdAsync(doctorId);

            if (schedules == null || !schedules.Any())
            {
                return new Response<IEnumerable<GetDoctorSchedule>>(null, "No schedules found for the doctor.");
            }

            var scheduleDtos = schedules.Select(schedule => schedule.ToGetDoctorScheduleDto()).ToList();

            return new Response<IEnumerable<GetDoctorSchedule>>(scheduleDtos);
        }


        public async Task<DeleteSchedulesResponse> DeleteScheduleByIdAsync(int scheduleId)
        {
            var isDeleted = await _doctorScheduleRepo.SoftDeleteAsync(scheduleId);

            if (!isDeleted)
            {
                return new DeleteSchedulesResponse(false, $"No schedule found with ID {scheduleId}.");
            }

            return new DeleteSchedulesResponse(true, $"Schedule with ID {scheduleId} was deleted successfully.");
        }



    }
}
