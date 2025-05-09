namespace SmartClinic.Application.Features.Appointments.Command.CreateAppointment;
public class CreateAppointmentRequest
{
    public int DoctorId { get; set; }
    public int SpecializationId { get; set; }
    public string AppointmentDate { get; set; } = null!;
    public TimeOnly StartTime { get; set; }
}
