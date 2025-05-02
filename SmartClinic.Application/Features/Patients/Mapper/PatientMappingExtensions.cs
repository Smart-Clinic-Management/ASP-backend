using SmartClinic.Application.Features.Patients.Query.DTOs;
using SmartClinic.Application.Features.Patients.Query.DTOs.GetPatient;
using SmartClinic.Application.Features.Patients.Query.DTOs.GetPatients;

namespace SmartClinic.Application.Mapping;

public static class PatientMappingExtensions
{
    public static GetAllPatientsResponse ToGetAllPatientsResponse(this Patient patient)
    { 
        return new GetAllPatientsResponse(
            id: patient.Id,
            firstName: patient.User?.FirstName ?? "",
            lastName: patient.User?.LastName ?? "",
            userPhoneNumber: patient.User?.PhoneNumber ?? "",
            age: patient.User.Age 
        );
    }

    public static GetDoctorSchedule ToGetPatientByIdResponse(this Patient patient)
    {
        return new GetDoctorSchedule(
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
