namespace SmartClinic.Application.Bases;
public static class ImageExtension
{
    public static string ToFullFilePath(this IFormFile image, string relativePath)
    {
        var FullFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", relativePath);

        return FullFilePath;
    }


    public static string ToRelativeFilePath(this IFormFile image)
    {
        var extenstion = Path.GetExtension(image.FileName);

        var filename = $"{Guid.NewGuid()}{extenstion}";
        var RelativeFilePath = Path.Combine("uploads", filename);

        return RelativeFilePath;
    }

}
