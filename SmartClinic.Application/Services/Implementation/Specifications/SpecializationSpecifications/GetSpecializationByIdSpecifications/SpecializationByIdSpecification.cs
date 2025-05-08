using SmartClinic.Application.Features.Specializations.Query.GetSpecialization;
using SmartClinic.Domain.Entities;
using SmartClinic.Application.Services.Implementation.Specifications;
using SmartClinic.Application.Features.Specializations.Command.DTOs.CreateSpecialization;
using SmartClinic.Application.Features.Doctors.Mapper;

namespace SmartClinic.Application.Services.Implementation.Specifications.SpecializationSpecifications.GetSpecializationByIdSpecifications;

public class SpecializationByIdSpecification : BaseSpecification<Specialization, GetSpecializationByIdResponse>
{
    public SpecializationByIdSpecification(int id, IHttpContextAccessor httpContextAccessor) : base(
        x => x.IsActive && x.Id == id
    )
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
}
