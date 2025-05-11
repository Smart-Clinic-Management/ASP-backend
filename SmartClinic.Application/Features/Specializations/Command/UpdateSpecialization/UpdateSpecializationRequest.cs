namespace SmartClinic.Application.Features.Specializations.Command.UpdateSpecialization;

public record UpdateSpecializationRequest(
 string? Name,
 string? Description,
 IFormFile? Image
);
