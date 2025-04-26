
namespace SmartClinic.Application.Features.Doctors.Command.DTOs.UpdateDoctor;
public record UpdateDoctorRequest(
       [Required] string Fname,
       [Required] string Lname,
       [Required, EmailAddress] string Email,
       IFormFile? Image,
       [Required] List<int> Specialization,
       [Required] DateOnly BirthDate,
       [Required] string Address,
       int? WaitingTime,
       string? Description

   );

