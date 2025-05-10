namespace SmartClinic.Application.Services.Implementation.FileHandlerService.Command;

public record FileValidation
{
    public required int MaxSize { get; set; }

    public required IList<string> AllowedExtenstions { get; set; } = [];

}
