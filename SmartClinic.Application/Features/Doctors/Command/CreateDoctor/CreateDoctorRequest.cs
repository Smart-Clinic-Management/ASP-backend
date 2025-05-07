namespace SmartClinic.Application.Features.Doctors.Command.CreateDoctor;

public record CreateDoctorRequest
    (
    string FirstName,
    string LastName,
    string Email,
    string Password,
    string PhoneNumber,
    IFormFile Image,
    int SpecializationId,
    DateOnly BirthDate,
    string Address,
    int WaitingTime,
    string? Description
    );
