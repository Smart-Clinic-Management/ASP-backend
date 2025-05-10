namespace SmartClinic.Application.Services.Implementation.Specifications.AppointmentSpecifications.CreateAppointmentSpecifications;
public class CreateAppointmentSpecification : BaseSpecification<Doctor>
{
    public CreateAppointmentSpecification(CreateAppointmentRequest appointmentRequest)
        : base(x => x.IsActive && x.Id == appointmentRequest.DoctorId)
    {
        var appointmentDate = appointmentRequest.AppointmentDate.ToDate();
        var appointmentDay = appointmentDate.DayOfWeek;

        AsNoTracking();
        AddInclude(x => x.User);
        AddInclude(x => x.DoctorSchedules
                .Where(s => s.DayOfWeek == appointmentDay &&
                           s.StartTime <= appointmentRequest.StartTime &&
                           s.EndTime >= appointmentRequest.StartTime.AddMinutes(s.SlotDuration)));

        AddInclude(x => x.Appointments
                            .Where(a => a.AppointmentDate == appointmentDate &&
                            a.Duration.StartTime <= appointmentRequest.StartTime &&
                            a.Duration.EndTime > appointmentRequest.StartTime));
    }
}
