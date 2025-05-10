namespace SmartClinic.Application.Services.Implementation.Specifications.SpecializationSpecifications.UpdateSpecializationSpecifications;
public class UpdateSpecializationSpecification(int id)
    : BaseSpecification<Specialization>(x => x.IsActive && x.Id == id)
{
}
