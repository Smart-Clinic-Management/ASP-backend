namespace SmartClinic.Application.Features.Doctors.Command.UpdateDoctor;
public record UpdateDoctorRequest(
    string? Fname,
    string? Lname,
    IFormFile? Image,
    DateOnly? BirthDate,
    string? Address,
    int? WaitingTime,
    string? Description,
    string? PhoneNumber
);



