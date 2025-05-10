namespace SmartClinic.Application.Features.Doctors.Query.GetDoctor;

public record GetDoctorByIdResponse(
     string FirstName,
     string LastName,
     string UserEmail,
     string PhoneNumber,
      int Age,
      DateOnly Birthdate,
     string Address,
    string? Description,
    int? WaitingTime,
    string? Image,
    int SpecializationId,
    string Specialization,
    IEnumerable<DoctorScheduleDto> Schedule
);
