namespace SmartClinic.Application.Services.Implementation.Specifications.AppointmentSpecifications.UpdateDoctorAppointmentSpecifications;
public class UpdateDoctorAppointmentSpecification(int doctorId, UpdateDoctorAppointmentRequest updateDoctorAppointment)
    : BaseSpecification<Appointment>(x => x.Id == updateDoctorAppointment.AppointmentId && x.DoctorId == doctorId)
{

}
