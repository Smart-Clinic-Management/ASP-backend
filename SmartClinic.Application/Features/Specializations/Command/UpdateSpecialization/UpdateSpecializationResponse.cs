namespace SmartClinic.Application.Features.Specializations.Command.UpdateSpecialization;

public record UpdateSpecializationResponse
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Image { get; set; }
}
