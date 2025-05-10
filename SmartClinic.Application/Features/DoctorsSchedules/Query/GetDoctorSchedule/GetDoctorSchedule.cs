namespace SmartClinic.Application.Features.DoctorsSchedules.Query.GetDoctorSchedule;

public record GetDoctorSchedule(
     int Id,
     int DoctorId,
     string DayOfWeek,
     TimeOnly StartTime,
     TimeOnly EndTime,
     int SlotDuration
);
