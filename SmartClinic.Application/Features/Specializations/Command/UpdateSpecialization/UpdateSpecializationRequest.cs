namespace SmartClinic.Application.Features.Specializations.Command.UpdateSpecialization;

public class UpdateSpecializationRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public bool? IsActive { get; set; }
    public IFormFile? Image { get; set; }


}
