namespace SmartClinic.Application.Features.Specializations.Query.GetSpecialization;

public record GetSpecializationByIdResponse(
    int Id,
    string Name,
    string? Description,
    string Image,
    IEnumerable<DoctorDto> Doctors
);

public record DoctorDto(
int Id,
string FirstName,
string? LastName,
string Image
);
