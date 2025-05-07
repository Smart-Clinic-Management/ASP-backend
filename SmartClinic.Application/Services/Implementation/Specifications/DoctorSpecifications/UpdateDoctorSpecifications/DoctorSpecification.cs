namespace SmartClinic.Application.Services.Implementation.Specifications.DoctorSpecifications.UpdateDoctorSpecifications;
public class UpdateDoctorSpecification : BaseSpecification<Doctor>
{

    public UpdateDoctorSpecification(int id)
        : base(
            x => x.IsActive && x.Id == id
            )
    {
        AddInclude(x => x.User);
    }
}
