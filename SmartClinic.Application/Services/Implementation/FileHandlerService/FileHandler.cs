namespace SmartClinic.Application.Services.Implementation.FileHandlerService
{
    public class FileHandler : IFileHandlerService
    {
        private readonly IHttpContextAccessor httpContext;

        public FileHandler(IHttpContextAccessor httpContext)
        {
            this.httpContext = httpContext;
        }

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

        public string GetFileURL(string path)
        {
            if (path == null) return null!;
            var request = httpContext.HttpContext?.Request;
            return $"{request!.Scheme}://{request!.Host}/{path.Replace("\\", "/")}";
        }

        public async Task<bool> RemoveImg(string path)
        {
            if (path == null)
                return false;
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", path);
            System.IO.File.Delete(filePath);
            return true;
        }
    }
}
