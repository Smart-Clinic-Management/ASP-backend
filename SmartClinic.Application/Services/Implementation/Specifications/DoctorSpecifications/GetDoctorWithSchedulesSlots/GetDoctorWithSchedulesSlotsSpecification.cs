namespace SmartClinic.Application.Services.Implementation.Specifications.DoctorSpecifications.GetDoctorWithSchedulesSlots;
public class GetDoctorWithSchedulesSlotsSpecification : BaseSpecification<Doctor>
{
    public GetDoctorWithSchedulesSlotsSpecification(GetDoctorWithSchedulesSlotsParams schedulesSlotsParams)
    : base(
            x => x.Id == schedulesSlotsParams.DoctorId && x.IsActive
            )
    {
        var endDate = schedulesSlotsParams.StartDate.AddDays(3);

        AsNoTracking();

        AddInclude(x => x.User);
        AddInclude(x => x.DoctorSchedules);
        AddInclude(x => x.Specialization);
        AddInclude(x => x.Appointments
                .Where(a => a.AppointmentDate >= schedulesSlotsParams.StartDate &&
                                a.AppointmentDate < endDate));

    }
}
