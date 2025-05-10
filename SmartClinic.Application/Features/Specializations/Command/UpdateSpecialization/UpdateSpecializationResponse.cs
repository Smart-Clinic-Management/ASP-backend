namespace SmartClinic.Application.Features.Specializations.Command.UpdateSpecialization;

public class UpdateSpecializationResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public string? Image { get; set; }

}
