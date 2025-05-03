namespace SmartClinic.Application.Features.Appointments.Command.CreateAppointment;
public class CreateAppointmentDto
{
    public int DoctorId { get; set; }
    public int SpecializationId { get; set; }
    public DateOnly AppointmentDate { get; set; }
    public TimeOnly StartTime { get; set; }
}
