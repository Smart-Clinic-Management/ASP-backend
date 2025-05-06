using System.Text.RegularExpressions;

namespace SmartClinic.Application.Bases;

public static class UserFormValidator
{
    public static bool IsValidPhoneNumber(string? phoneNumber)
    {
        if (phoneNumber is null) return true;

        string pattern = @"^\+20[0-9]{10}$";

        return Regex.IsMatch(phoneNumber, pattern);
    }


    public static bool IsValidImageExtension(IFormFile? file)
    {
        if (file is null) return true;

        var fileName = Path.GetFileName(file.FileName);
        string pattern = @"^.*\.(jpg|jpeg|png)$";

        return Regex.IsMatch(fileName, pattern, RegexOptions.IgnoreCase);
    }

    public static bool IsValidDoctorAge(DateOnly? birthDate)
    {
        if (birthDate is null) return true;

        return birthDate.Value.Year <= 1999;
    }
}
