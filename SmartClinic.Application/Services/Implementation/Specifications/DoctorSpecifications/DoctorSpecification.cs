using SmartClinic.Application.Features.Doctors.Mapper;
using SmartClinic.Application.Features.Doctors.Query.GetDoctors;

namespace SmartClinic.Application.Services.Implementation.Specifications.DoctorSpecifications;
public class DoctorSpecification : BaseSpecification<Doctor, GetAllDoctorsResponse>
{


    public DoctorSpecification(GetAllDoctorsParams doctorsParams, IHttpContextAccessor httpContextAccessor)
        : base(
        x => x.IsActive
            && (string.IsNullOrWhiteSpace(doctorsParams.DoctorName)
            || doctorsParams.DoctorName.Contains(x.User.FirstName))

          && (string.IsNullOrWhiteSpace(doctorsParams.Specialization)
          || doctorsParams.Specialization.Contains(x.Specialization.Name))
             )
    {
        AddPagination(doctorsParams.PageIndex, doctorsParams.PageSize);

        if (doctorsParams.IsDescending)
            AddOrderByDescending(doctorsParams.OrderBy);
        else
            AddOrderBy(doctorsParams.OrderBy);

        AddSelect(x => new GetAllDoctorsResponse()
        {
            FirstName = x.User.FirstName,
            LastName = x.User.LastName,
            Id = x.User.Id,
            Image = DoctorMappingExtensions.GetImgUrl(x.User.ProfileImage, httpContextAccessor),
            Specialization = x.Specialization.Name,
            Age = x.User.Age,
            Description = x.Description,
            WaitingTime = x.WaitingTime
        });
    }


}
