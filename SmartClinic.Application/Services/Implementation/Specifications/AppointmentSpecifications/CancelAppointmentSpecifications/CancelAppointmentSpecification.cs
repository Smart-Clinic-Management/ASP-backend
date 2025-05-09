namespace SmartClinic.Application.Services.Implementation.Specifications.AppointmentSpecifications.CancelAppointmentSpecifications;
public class CancelAppointmentSpecification(int patientId, int appointmentId)
    : BaseSpecification<Appointment>(x => x.PatientId == patientId && x.Id == appointmentId)
{

}
