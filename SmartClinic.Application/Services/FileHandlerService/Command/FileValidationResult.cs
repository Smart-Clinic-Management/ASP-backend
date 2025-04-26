namespace SmartClinic.Application.Services.FileHandlerService.Command
{
    public class FileValidationResult
    {
        public bool Success { get; set; }

        public string? Error { get; set; } = null;

        public string? RelativeFilePath { get; set; } = null;
        public string? FullFilePath { get; set; } = null;
    }
}
