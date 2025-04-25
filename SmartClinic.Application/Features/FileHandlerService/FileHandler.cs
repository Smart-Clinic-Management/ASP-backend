using Microsoft.AspNetCore.Http;
using SmartClinic.Application.Features.FileHandlerService.Command;

namespace SmartClinic.Application.Features.FileHandlerService
{
    public class FileHandler
    {

        public async Task<FileValidationResult> HanldeFile(IFormFile file, FileValidation options)
        {
            var result = new FileValidationResult() { Success = false };

            var extenstion = Path.GetExtension(file.FileName);

            if (!options.AllowedExtenstions.Contains(extenstion))
            {
                result.Error = $"extenstion type must be in[ {string.Join(',', options.AllowedExtenstions)} ]";
            }

            if (file.Length > options.MaxSize)
            {
                result.Error = $"File size must be less than {options.MaxSize} bytes";
            }

            if (file.Length == 0)
            {
                result.Error = "file not provided";
            }


            if (result.Error?.Length > 0)
            {
                return result;
            }

            var filename = $"{Guid.NewGuid()}{extenstion}";
            result.Success = true;
            result.RelativeFilePath = Path.Combine("uploads", filename);
            result.FullFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", result.RelativeFilePath);

            return result;
        }

        public async Task SaveFile(IFormFile file, string fileFullPath)
        {
            using (var stream = new FileStream(fileFullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
        }
    }
}
