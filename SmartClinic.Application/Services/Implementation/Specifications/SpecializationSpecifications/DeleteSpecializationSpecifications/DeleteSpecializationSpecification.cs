namespace SmartClinic.Application.Services.Implementation.Specifications.SpecializationSpecifications.DeleteSpecializationSpecifications;
public class DeleteSpecializationSpecification : BaseSpecification<Specialization>
{
    public DeleteSpecializationSpecification(int specializationId)
        : base(x => x.Id == specializationId && x.IsActive)
    {
        AddInclude(x => x.Doctors.Where(d => d.IsActive));
    }
}
