using SmartClinic.Application.Features.Doctors.Mapper;
using SmartClinic.Application.Features.Doctors.Query.GetDoctor;

namespace SmartClinic.Application.Services.Implementation.Specifications.DoctorSpecifications.GetDoctorByIdSpecifications;
public class DoctorByIdSpecification : BaseSpecification<Doctor, GetDoctorByIdResponse>
{
    public DoctorByIdSpecification(int id, IHttpContextAccessor httpContextAccessor) : base(
      x => x.IsActive && x.Id == id
      )
    {
        AddSelect(x => new GetDoctorByIdResponse(
            x.User.FirstName,
            x.User.LastName,
            x.User.Email,
            x.User.PhoneNumber,
            x.User.Age,
            x.User.BirthDate,
            x.User.Address,
            x.Description,
            x.WaitingTime,
            DoctorMappingExtensions.GetImgUrl(x.User.ProfileImage, httpContextAccessor),
            x.SpecializationId,
            x.Specialization.Name
            ));
    }
}
