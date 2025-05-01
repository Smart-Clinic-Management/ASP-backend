using System.ComponentModel.DataAnnotations;

namespace SmartClinic.Application.Features.Doctors.Command.DTOs.CreateDoctor;

public record CreateDoctorRequest(
 [Required] string Fname,
 string Lname,
 [EmailAddress] string Email,
 [Required] IFormFile Image,
 [Required] int SpecializationId,
 DateOnly BirthDate,
 [Required] string Address,
 int? WaitingTime,
 string? Description
);
