using System.ComponentModel.DataAnnotations;

namespace SmartClinic.Application.Features.Patients.Query.DTOs.GetPatient;

public record GetPatientByIdResponse(
[Required] int id,
[Required] string firstName,
[Required] string lastName,
[EmailAddress] string userEmail,
[Required] string userPhoneNumber,
[Required] byte age,
[Required] string address,
string? image,
string? medicalHistory
);
