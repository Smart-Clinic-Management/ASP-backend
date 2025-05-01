using System.ComponentModel.DataAnnotations;

namespace SmartClinic.Application.Features.Doctors.Command.DTOs.CreateDoctor;

public record CreateDoctorResponse(
[Required] string Fname,
string Lname,
[EmailAddress] string Email,
[Required] string Image,
[Required] int SpecializationId,
DateOnly BirthDate,
[Required] string Address,
int? WaitingTime,
string? Description
);
