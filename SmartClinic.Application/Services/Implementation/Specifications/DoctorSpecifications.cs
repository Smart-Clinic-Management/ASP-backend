
using System.Linq.Expressions;

namespace SmartClinic.Application.Services.Implementation.Specifications;
public class DoctorSpecifications : ISpecification<Doctor>
{
    public Expression<Func<Doctor, bool>>? Criteria { get; } = null;

    public DoctorSpecifications(GetAllDoctorsParams doctorsParams)
    {
        if (doctorsParams is not null)
            Criteria = x => x.IsActive
            && (string.IsNullOrWhiteSpace(doctorsParams.DoctorName)
            || doctorsParams.DoctorName.Contains(x.User.FirstName))

          && (string.IsNullOrWhiteSpace(doctorsParams.Specialization)
          || doctorsParams.Specialization.Contains(x.Specialization.Name));
    }
}
