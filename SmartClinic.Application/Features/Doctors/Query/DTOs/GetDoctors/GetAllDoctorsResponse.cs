using System.ComponentModel.DataAnnotations;

namespace SmartClinic.Application.Features.Doctors.Query.DTOs.GetDoctors;

public record GetAllDoctorsResponse(
 [Required] int Id,
 [Required] string firstName,
[Required] string lastName,
string? image,
[Required] string Specialization
);
