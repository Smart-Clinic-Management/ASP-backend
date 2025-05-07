namespace SmartClinic.Application.Services.Implementation.Specifications.DoctorSpecifications.DeleteDoctorSpecifications;
public class DeleteDoctorSpecification : BaseSpecification<Doctor>
{
    public DeleteDoctorSpecification(int id)
        : base(x => x.IsActive && x.Id == id)
    {
        var currentDate = DateOnly.FromDateTime(DateTime.Now);

        AddInclude(x => x.Appointments
                        .Where(x => x.AppointmentDate >= currentDate
                          && x.Status == AppointmentStatus.Pending)
        , x => x.User
        , x => x.DoctorSchedules);
    }
}
