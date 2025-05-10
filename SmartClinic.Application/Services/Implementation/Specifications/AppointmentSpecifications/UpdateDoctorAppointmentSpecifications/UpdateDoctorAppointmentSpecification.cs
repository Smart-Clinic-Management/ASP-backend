namespace SmartClinic.Application.Services.Implementation.Specifications.AppointmentSpecifications.UpdateDoctorAppointmentSpecifications;
public class UpdateDoctorAppointmentSpecification : BaseSpecification<Appointment>
{
    public UpdateDoctorAppointmentSpecification(int doctorId, UpdateAppointmentRequest updateDoctorAppointment)
        : base(x => x.Id == updateDoctorAppointment.AppointmentId && x.DoctorId == doctorId)
    {
        AddInclude($"{nameof(Appointment.Patient)}.{nameof(Appointment.Patient.User)}");
    }
}
