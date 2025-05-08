namespace SmartClinic.Application.Services.Implementation.Specifications.AppointmentSpecifications.CreateAppointmentSpecifications;
public class CreateAppointmentSpecification : BaseSpecification<Doctor>
{
    public CreateAppointmentSpecification(CreateAppointmentDto appointmentDto)
        : base(x => x.IsActive && x.Id == appointmentDto.DoctorId)
    {

        AsNoTracking();
        AddInclude(x => x.DoctorSchedules
                .Where(s => s.DayOfWeek == appointmentDto.AppointmentDate.DayOfWeek &&
                           s.StartTime <= appointmentDto.StartTime &&
                           s.EndTime >= appointmentDto.StartTime.AddMinutes(s.SlotDuration)));

        AddInclude(x => x.Appointments
                            .Where(a => a.AppointmentDate == appointmentDto.AppointmentDate &&
                            a.Duration.StartTime <= appointmentDto.StartTime &&
                            a.Duration.EndTime > appointmentDto.StartTime));
    }
}
