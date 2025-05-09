using SmartClinic.Application.Features.Appointments.Command.UpdateAppointmnet;

namespace SmartClinic.Application.Services.Implementation.Specifications.AppointmentSpecifications.UpdateDoctorAppointmentSpecifications;
public class UpdateDoctorAppointmentSpecification(int doctorId, UpdateAppointmentRequest updateDoctorAppointment)
    : BaseSpecification<Appointment>(x => x.Id == updateDoctorAppointment.AppointmentId && x.DoctorId == doctorId)
{

}
