using System.ComponentModel.DataAnnotations;

namespace SmartClinic.Application.Features.Doctors.Command.DTOs.UpdateDoctor;
public record UpdateDoctorResponse(
  [Required] string FirstName,
    string LastName,
   [Required] string PhoneNumber,
   [EmailAddress] string Email,
    DateOnly BirthDate,
   [Required] string Address,
    string? Description,
    int? WaitingTime,
   [Required] List<int> Specializations
    );

