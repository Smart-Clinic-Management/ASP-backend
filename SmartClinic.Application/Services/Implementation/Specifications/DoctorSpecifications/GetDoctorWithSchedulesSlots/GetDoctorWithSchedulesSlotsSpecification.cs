namespace SmartClinic.Application.Services.Implementation.Specifications.DoctorSpecifications.GetDoctorWithSchedulesSlots;
public class GetDoctorWithSchedulesSlotsSpecification : BaseSpecification<Doctor>
{
    public GetDoctorWithSchedulesSlotsSpecification(GetDoctorWithSchedulesSlotsParams schedulesSlotsParams)
    : base(
            x => x.Id == schedulesSlotsParams.DoctorId && x.IsActive
            )
    {
        var startdate = schedulesSlotsParams.StartDate.ToDate();
        var endDate = startdate.AddDays(3);

        AsNoTracking();

        AddInclude(x => x.User);
        AddInclude(x => x.DoctorSchedules);
        AddInclude(x => x.Specialization);
        AddInclude(x => x.Appointments
                .Where(a => a.AppointmentDate >= startdate &&
                                a.AppointmentDate < endDate));

    }
}
