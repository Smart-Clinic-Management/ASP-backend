using Microsoft.AspNetCore.Http;
using SmartClinic.Application.Services.Implementation.FileHandlerService.Command;

namespace SmartClinic.Application.Services.Interfaces
{
    public interface IFileHandlerService
    {
        Task<FileValidationResult> HanldeFile(IFormFile file, FileValidation options);

        Task SaveFile(IFormFile file, string path);

        string GetFileURL(string filePath);
    }
}
