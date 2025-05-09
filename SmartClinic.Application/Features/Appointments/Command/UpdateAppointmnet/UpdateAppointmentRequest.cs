namespace SmartClinic.Application.Features.Appointments.Command.UpdateAppointmnet;
public record UpdateAppointmentRequest(int AppointmentId, AppointmentStatus Status);
