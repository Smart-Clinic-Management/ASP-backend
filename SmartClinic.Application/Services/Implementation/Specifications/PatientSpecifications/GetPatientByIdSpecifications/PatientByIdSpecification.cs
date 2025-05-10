
using SmartClinic.Application.Services.Implementation.Specifications;
using Microsoft.AspNetCore.Http;
using SmartClinic.Application.Features.Patients.Mapper;

namespace SmartClinic.Application.Services.Implementation.Specifications.PatientSpecifications.GetPatientByIdSpecifications;

public class PatientByIdSpecification : BaseSpecification<Patient, GetPatientByIdResponse>
{
    public PatientByIdSpecification(int id, IHttpContextAccessor httpContextAccessor)
        : base(x => x.IsActive && x.Id == id)
    {
        AddSelect(x => new GetPatientByIdResponse(
            x.Id,
            x.User.FirstName,
            x.User.LastName,
            x.User.Email,
            x.User.PhoneNumber,
            x.User.Age,
            x.User.Address,
            DoctorMappingExtensions.GetImgUrl(x.User.ProfileImage, httpContextAccessor),
            x.MedicalHistory
        ));
    }

    public PatientByIdSpecification(int id)
        : base(x => x.IsActive && x.Id == id)
    {

    }
}
