namespace SmartClinic.Application.Features.DoctorsSchedules.DTOs;
public record DoctorScheduleDto
(
    int ScheduleId,
    DayOfWeek DayNumber,
    string Day,
    TimeOnly StartTime,
    TimeOnly EndTime,
    int SlotDuration
    );