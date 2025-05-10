namespace SmartClinic.Application.Features.Patients.Mapper;

public static class PatientMappingExtensions
{
    public static GetPatientByIdResponse ToGetPatientByIdResponse(this Patient patient)
    {
        return new GetPatientByIdResponse(
            id: patient.Id,
            firstName: patient.User.FirstName,
            lastName: patient.User.LastName,
            userEmail: patient.User.Email,
            userPhoneNumber: patient.User.PhoneNumber,
            age: patient.User.Age,
            address: patient.User.Address,
            image: patient.User.ProfileImage,
            medicalHistory: patient.MedicalHistory
        );
    }

}
