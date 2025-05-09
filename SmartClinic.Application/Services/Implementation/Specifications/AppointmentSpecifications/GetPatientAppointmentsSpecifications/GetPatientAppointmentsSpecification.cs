using SmartClinic.Application.Features.Appointments.Query.PatientAppointments;

namespace SmartClinic.Application.Services.Implementation.Specifications.AppointmentSpecifications.GetPatientAppointmentsSpecifications;
public class GetPatientAppointmentsSpecification : BaseSpecification<Appointment>
{
    public GetPatientAppointmentsSpecification(int patientId, GetPatientAppointmentParams appointmentParams)
        : base(x => x.PatientId == patientId)
    {
        AddInclude(x => x.Doctor);
        AddInclude($"{nameof(Appointment.Doctor)}.{nameof(Appointment.Doctor.User)}");

        AddPagination(appointmentParams.PageIndex, appointmentParams.PageSize);

        if (appointmentParams.IsDescending)
            AddOrderByDescending(appointmentParams.OrderBy);
        else
            AddOrderBy(appointmentParams.OrderBy);
    }
}
