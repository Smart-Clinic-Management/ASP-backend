namespace SmartClinic.Application.Services.Implementation.Specifications.AppointmentSpecifications.CancelAppointmentSpecifications;
public class CancelAppointmentSpecification : BaseSpecification<Appointment>
{
    public CancelAppointmentSpecification(int patientId, int appointmentId)
        : base(x => x.PatientId == patientId && x.Id == appointmentId)
    {
        AddInclude($"{nameof(Appointment.Doctor)}.{nameof(Appointment.Doctor.User)}");
    }
}
