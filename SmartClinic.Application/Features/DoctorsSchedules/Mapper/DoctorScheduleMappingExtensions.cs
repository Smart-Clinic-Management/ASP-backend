using SmartClinic.Application.Features.DoctorsSchedules.Query.GetDoctorSchedule;

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

}
