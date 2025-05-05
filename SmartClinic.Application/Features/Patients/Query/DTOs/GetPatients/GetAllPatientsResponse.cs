using System.ComponentModel.DataAnnotations;

namespace SmartClinic.Application.Features.Patients.Query.DTOs.GetPatients;

public record GetAllPatientsResponse(
[Required] int id,
[Required] string firstName,
[Required] string lastName,
[Required] string userPhoneNumber,
[Required] int age
);
