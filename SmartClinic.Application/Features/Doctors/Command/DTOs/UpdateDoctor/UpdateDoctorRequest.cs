using System.ComponentModel.DataAnnotations;

namespace SmartClinic.Application.Features.Doctors.Command.DTOs.UpdateDoctor;
public record UpdateDoctorRequest(
  [Required] string FirstName,
    string LastName,
   [Required] string PhoneNumber,
   [EmailAddress] string Email,
   [DataType(DataType.Password)] string Password,
    DateOnly BirthDate,
   [Required] string Address,
    string? Description,
    int? WaitingTime,
   [Required] List<int> Specializations
    );

