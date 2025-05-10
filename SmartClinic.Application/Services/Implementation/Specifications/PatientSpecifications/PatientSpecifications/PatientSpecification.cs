using Microsoft.AspNetCore.Http;
using SmartClinic.Application.Features.Patients.Query.GetPatients;

namespace SmartClinic.Application.Services.Implementation.Specifications.PatientSpecifications.GetPatients
{
    public class PatientSpecification : BaseSpecification<Patient, GetAllPatientsResponse>
    {
        public PatientSpecification(GetAllPatientsParams patientsParams, IHttpContextAccessor httpContextAccessor)
            : base(
            x => x.IsActive
                && (string.IsNullOrWhiteSpace(patientsParams.PatientName)
                || (x.User.FirstName + x.User.LastName).Contains(patientsParams.PatientName))
            )
        {
            AddPagination(patientsParams.PageIndex, patientsParams.PageSize);

            if (patientsParams.IsDescending)
                AddOrderByDescending(patientsParams.OrderBy);
            else
                AddOrderBy(patientsParams.OrderBy);

            AddSelect(x => new GetAllPatientsResponse()
            {
                Id = x.User.Id,
                FirstName = x.User.FirstName,
                LastName = x.User.LastName,
                UserPhoneNumber = x.User.PhoneNumber,
                Age = x.User.Age
            });
        }
    }
}
