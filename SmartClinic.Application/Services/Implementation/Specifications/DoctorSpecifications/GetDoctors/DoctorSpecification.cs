namespace SmartClinic.Application.Services.Implementation.Specifications.DoctorSpecifications.GetDoctors;
public class DoctorSpecification : BaseSpecification<Doctor, GetAllDoctorsResponse>
{


    public DoctorSpecification(GetAllDoctorsParams doctorsParams, IHttpContextAccessor httpContextAccessor)
        : base(
        x => x.IsActive
            && (string.IsNullOrWhiteSpace(doctorsParams.DoctorName)
            || (x.User.FirstName + x.User.LastName).Contains(doctorsParams.DoctorName))

          && (string.IsNullOrWhiteSpace(doctorsParams.Specialization)
          || x.Specialization.Name.Contains(doctorsParams.Specialization))
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
