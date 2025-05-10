namespace SmartClinic.Application.Features.Specializations.Command.CreateSpecialization;


public record CreateSpecializationRequest(
    string Name,
    string Description,
    IFormFile Image
    );
