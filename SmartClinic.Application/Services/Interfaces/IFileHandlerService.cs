namespace SmartClinic.Application.Services.Interfaces;

public interface IFileHandlerService
{
    Task<FileValidationResult> HandleFile(IFormFile file, FileValidation options);

    Task SaveFile(IFormFile file, string path);

    string GetFileURL(string filePath);

    Task<bool> RemoveImg(string path);

    Task UpdateImg(IFormFile file, string newImgRelativePath, string oldImgRelativePath);
}
