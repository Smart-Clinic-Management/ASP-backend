using SmartClinic.Domain.Entities;
using SmartClinic.Application.Features.Patients.Query.DTOs.GetDoctorSchedule;
using SmartClinic.Application.Features.DoctorSchedule.Command.CreateDoctorSchedule;
using SmartClinic.Application.Features.DoctorSchedule.Command.UpdateDoctorSchedule;
using SmartClinic.Application.Features.DoctorSchedule.Command.UpdateDoctorSchedule.SmartClinic.Application.Features.DoctorSchedule.Command.UpdateDoctorSchedule;

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
                EndTime: schedule.EndTime,
                SlotDuration: schedule.SlotDuration
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

        public static DoctorSchedule ToEntity(this CreateDoctorScheduleRequest request)
        {
            return new DoctorSchedule
            {
                DoctorId = request.DoctorId,
                DayOfWeek = request.DayOfWeek,
                StartTime = request.StartTime,
                EndTime = request.EndTime,
                SlotDuration = request.SlotDuration
            };
        }

        public static void UpdateFromRequest(this DoctorSchedule entity, UpdateDoctorScheduleRequest request)
        {
            if (request.Day.HasValue)
            {
                entity.DayOfWeek = request.Day.Value;
            }

            if (request.StartTime.HasValue)
            {
                entity.StartTime = request.StartTime.Value;
            }

            if (request.EndTime.HasValue)
            {
                entity.EndTime = request.EndTime.Value;
            }

            entity.DoctorId = request.DoctorId;
            entity.SlotDuration = request.SlotDuration;
        }




    }
}
