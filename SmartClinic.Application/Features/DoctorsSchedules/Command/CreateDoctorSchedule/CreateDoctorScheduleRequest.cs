namespace SmartClinic.Application.Features.DoctorsSchedules.Command.CreateDoctorSchedule;

public record CreateDoctorScheduleRequest(
   int DoctorId,
   DayOfWeek DayOfWeek,
   TimeOnly StartTime,
   TimeOnly EndTime,
   int SlotDuration
);
