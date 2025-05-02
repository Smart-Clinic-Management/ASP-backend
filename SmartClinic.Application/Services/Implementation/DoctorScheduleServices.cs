using SmartClinic.Application.Features.Patients.Query.DTOs.GetDoctorSchedule;
using SmartClinic.Infrastructure.Repos;
using SmartClinic.Application.Services.Interfaces;
using SmartClinic.Infrastructure.Interfaces;
using SmartClinic.Application.MappingExtensions;
using SmartClinic.Application.Features.Patients.Query.DTOs.GetDoctorSchedule;

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


    }
}
