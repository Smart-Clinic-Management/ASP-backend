
namespace SmartClinic.Application.Features.Patients.Query.GetPatient;

public record GetPatientByIdResponse(
int id,
string firstName,
string lastName,
string userEmail,
string userPhoneNumber,
int age,
string address,
string? image,
string? medicalHistory
);
