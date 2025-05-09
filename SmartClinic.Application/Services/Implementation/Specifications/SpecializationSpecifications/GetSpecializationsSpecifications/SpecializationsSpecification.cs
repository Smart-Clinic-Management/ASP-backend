namespace SmartClinic.Application.Services.Implementation.Specifications.SpecializationSpecifications.GetSpecializationsSpecifications;

public class SpecializationsSpecification : BaseSpecification<Specialization, GetAllSpecializationsResponse>
{
    public SpecializationsSpecification(GetAllSpecializationsParams specializationsParams, IHttpContextAccessor httpContextAccessor)
        : base(
        x => x.IsActive
            && (string.IsNullOrWhiteSpace(specializationsParams.SpecializationName)
            || x.Name.Contains(specializationsParams.SpecializationName))
        )
    {
        AddPagination(specializationsParams.PageIndex, specializationsParams.PageSize);

        if (specializationsParams.IsDescending)
            AddOrderByDescending(specializationsParams.OrderBy);
        else
            AddOrderBy(specializationsParams.OrderBy);

        AddSelect(x => new GetAllSpecializationsResponse(
            x.Id,
            x.Name,
            x.Description,
           DoctorMappingExtensions.GetImgUrl(x.Image, httpContextAccessor)
        ));
    }
}
