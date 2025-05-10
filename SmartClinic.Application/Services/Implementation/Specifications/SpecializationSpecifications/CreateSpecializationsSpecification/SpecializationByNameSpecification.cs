namespace SmartClinic.Application.Services.Implementation.Specifications.SpecializationSpecifications.CreateSpecializationsSpecification;

public class SpecializationByNameSpecification(string name)
    : BaseSpecification<Specialization>(x => x.Name.Contains(name) && x.IsActive)
{
}