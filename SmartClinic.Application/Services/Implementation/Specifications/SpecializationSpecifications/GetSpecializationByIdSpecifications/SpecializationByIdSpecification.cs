namespace SmartClinic.Application.Services.Implementation.Specifications.SpecializationSpecifications.GetSpecializationByIdSpecifications;

public class SpecializationByIdSpecification : BaseSpecification<Specialization, GetSpecializationByIdResponse>
{
    public SpecializationByIdSpecification(int id, IHttpContextAccessor httpContextAccessor)
        : base(x => x.IsActive && x.Id == id)
    {
        AddSelect(x => new GetSpecializationByIdResponse(
            x.Id,
            x.Name,
            x.Description,
            DoctorMappingExtensions.GetImgUrl(x.Image, httpContextAccessor),
            x.Doctors.Select(d => new DoctorDto(
                d.Id,
                d.User.FirstName,
                d.User.LastName,
                DoctorMappingExtensions.GetImgUrl(d.User.ProfileImage, httpContextAccessor)
            ))
        ));
    }

    public SpecializationByIdSpecification(int id)
        : base(x => x.IsActive && x.Id == id)
    {

    }
}
