using SmartClinic.Application.Features.Appointments.Query.PatientAppointments;

namespace SmartClinic.Application.Services.Implementation.Specifications.AppointmentSpecifications.GetPatientAppointmentsSpecifications;
public class GetPatientAppointmentsSpecification : BaseSpecification<Appointment>
{
    public GetPatientAppointmentsSpecification(int patientId, GetPatientAppointmentsParams appointmentsParams)
        : base(x => x.PatientId == patientId)
    {
        AddInclude(x => x.Doctor);
        AddInclude($"{nameof(Appointment.Doctor)}.{nameof(Appointment.Doctor.User)}");

        AddPagination(appointmentsParams.PageIndex, appointmentsParams.PageSize);

        if (appointmentsParams.IsDescending)
            AddOrderByDescending(appointmentsParams.OrderBy);
        else
            AddOrderBy(appointmentsParams.OrderBy);
    }
}
