using SmartClinic.Application.Features.DoctorSchedule.Command.UpdateDoctorSchedule;
using SmartClinic.Application.Features.DoctorsSchedules.Command.CreateDoctorSchedule;
using SmartClinic.Application.Features.DoctorsSchedules.Query.DTOs.GetDoctorSchedule;

namespace SmartClinic.Application.Features.DoctorsSchedules.Mapper;

public static class DoctorScheduleMappingExtensions
{
    public static GetDoctorSchedule ToGetDoctorScheduleDto(this Domain.Entities.DoctorSchedule schedule)
    {
        return new GetDoctorSchedule(
            Id: schedule.Id,
            DoctorId: schedule.DoctorId,
            DayOfWeek: schedule.DayOfWeek.ToString(),
            StartTime: schedule.StartTime,
            EndTime: schedule.EndTime,
            SlotDuration: schedule.SlotDuration
        );
    }

    //public static IEnumerable<GetDoctorSchedule> ToGetDoctorScheduleDtoList(this IEnumerable<Domain.Entities.DoctorSchedule> schedules)
    //{
    //    return schedules.Select(schedule => schedule.ToGetDoctorScheduleDto());
    //}

    //public static IEnumerable<GetDoctorSchedule> ToGetDoctorScheduleByDoctorIdDtoList(this IEnumerable<Domain.Entities.DoctorSchedule> schedules, int doctorId)
    //{
    //    return schedules
    //        .Where(schedule => schedule.DoctorId == doctorId)
    //        .ToGetDoctorScheduleDtoList();
    //}

    public static Domain.Entities.DoctorSchedule ToEntity(this CreateDoctorScheduleRequest request)
    {
        return new Domain.Entities.DoctorSchedule
        {
            DoctorId = request.DoctorId,
            DayOfWeek = request.DayOfWeek,
            StartTime = request.StartTime,
            EndTime = request.EndTime,
            SlotDuration = request.SlotDuration
        };
    }

    public static void UpdateFromRequest(this Domain.Entities.DoctorSchedule entity, UpdateDoctorScheduleRequest request)
    {
        if (request.Day.HasValue)
            entity.DayOfWeek = request.Day.Value;

        if (request.StartTime.HasValue)
            entity.StartTime = request.StartTime.Value;

        if (request.EndTime.HasValue)
            entity.EndTime = request.EndTime.Value;

        entity.DoctorId = request.DoctorId;
        entity.SlotDuration = request.SlotDuration;
    }




}
