namespace SmartClinic.Application.Features.Profile.Command;

public record class ImgUpdateRequest
{
    public required IFormFile File { get; set; }
}
