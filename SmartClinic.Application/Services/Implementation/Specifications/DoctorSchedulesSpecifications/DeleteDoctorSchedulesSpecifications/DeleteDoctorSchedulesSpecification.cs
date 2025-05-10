namespace SmartClinic.Application.Services.Implementation.Specifications.DoctorSchedulesSpecifications.DeleteDoctorSchedulesSpecifications;
public class DeleteDoctorSchedulesSpecification : BaseSpecification<Doctor>
{
    public DeleteDoctorSchedulesSpecification(DeleteDoctorScheduleRequest deleteDoctorSchedule)
        : base(x => x.IsActive && x.Id == deleteDoctorSchedule.DoctorId)
    {
        var currentDate = DateOnly.FromDateTime(DateTime.Now);

        AddInclude(x => x.DoctorSchedules.Where(s => s.Id == deleteDoctorSchedule.ScheduleId));
        AddInclude(x => x.Appointments.Where(a => a.AppointmentDate >= currentDate));
    }
}
