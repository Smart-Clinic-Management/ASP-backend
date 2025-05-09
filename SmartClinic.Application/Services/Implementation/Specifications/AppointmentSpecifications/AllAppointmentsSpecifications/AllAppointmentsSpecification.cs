using SmartClinic.Application.Features.Appointments.Query.AllAppointments;

namespace SmartClinic.Application.Services.Implementation.Specifications.AppointmentSpecifications.AllAppointmentsSpecifications;
public class AllAppointmentsSpecification : BaseSpecification<Appointment>
{
    public AllAppointmentsSpecification(AllAppointmentsParams appointmentsParams)
        : base(x => (appointmentsParams.DoctorId == null || x.DoctorId == appointmentsParams.DoctorId)
        && (appointmentsParams.PatientId == null || appointmentsParams.PatientId == x.PatientId))
    {
        AddInclude(x => x.Specialization);

        AddInclude(x => x.Doctor);
        AddInclude($"{nameof(Appointment.Doctor)}.{nameof(Appointment.Doctor.User)}");

        AddInclude(x => x.Patient);
        AddInclude($"{nameof(Appointment.Patient)}.{nameof(Appointment.Patient.User)}");

        AddPagination(appointmentsParams.PageIndex, appointmentsParams.PageSize);

        if (appointmentsParams.IsDescending)
            AddOrderByDescending(appointmentsParams.OrderBy);
        else
            AddOrderBy(appointmentsParams.OrderBy);
    }
}
