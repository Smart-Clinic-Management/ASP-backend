namespace SmartClinic.Application.Features.DoctorsSchedules.Command.CreateDoctorSchedule;

public record CreateDoctorScheduleResponse(
  int Id,
  string Message = "Schedule created successfully"
);
