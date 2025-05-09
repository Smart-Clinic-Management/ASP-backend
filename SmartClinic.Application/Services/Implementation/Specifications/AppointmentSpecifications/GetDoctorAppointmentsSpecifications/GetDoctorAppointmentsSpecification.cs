using SmartClinic.Application.Features.Appointments.Query.DoctorAppointments;

namespace SmartClinic.Application.Services.Implementation.Specifications.AppointmentSpecifications.GetDoctorAppointmentsSpecifications;
public class GetDoctorAppointmentsSpecification : BaseSpecification<Appointment>
{
    public GetDoctorAppointmentsSpecification(int doctorId, GetDoctorAppointmentsParams appointmentsParams)
        : base(x => x.DoctorId == doctorId)
    {
        AddInclude(x => x.Patient);
        AddInclude($"{nameof(Appointment.Patient)}.{nameof(Appointment.Patient.User)}");

        AddPagination(appointmentsParams.PageIndex, appointmentsParams.PageSize);

        if (appointmentsParams.IsDescending)
            AddOrderByDescending(appointmentsParams.OrderBy);
        else
            AddOrderBy(appointmentsParams.OrderBy);
    }
}
