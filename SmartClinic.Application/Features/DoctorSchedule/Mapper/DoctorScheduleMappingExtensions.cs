using SmartClinic.Domain.Entities;
using SmartClinic.Application.Features.Patients.Query.DTOs.GetDoctorSchedule;

namespace SmartClinic.Application.MappingExtensions
{
    public static class DoctorScheduleMappingExtensions
    {
        public static GetDoctorSchedule ToGetDoctorScheduleDto(this DoctorSchedule schedule)
        {
            return new GetDoctorSchedule(
                DoctorId: schedule.DoctorId,
                DayOfWeek: schedule.DayOfWeek,
                StartTime: schedule.StartTime,
                EndTime: schedule.EndTime
            );
        }

        public static IEnumerable<GetDoctorSchedule> ToGetDoctorScheduleDtoList(this IEnumerable<DoctorSchedule> schedules)
        {
            return schedules.Select(schedule => schedule.ToGetDoctorScheduleDto());
        }

        public static IEnumerable<GetDoctorSchedule> ToGetDoctorScheduleByDoctorIdDtoList(this IEnumerable<DoctorSchedule> schedules, int doctorId)
        {
            return schedules
                .Where(schedule => schedule.DoctorId == doctorId)
                .ToGetDoctorScheduleDtoList();
        }
    }
}
