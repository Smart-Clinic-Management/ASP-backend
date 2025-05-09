namespace SmartClinic.Application.Bases;
public static class GlobalValidator
{
    public static bool BeAValidDateOnly(string date) => DateOnly.TryParseExact(date, "yyyy-MM-dd", out _);

    public static string ValidDateRegex() => @"^\d{4}-(0[1-9]|1[0-2])-(0[1-9]|[12]\d|3[01])$";

    public static DateOnly ToDate(this string date) => DateOnly.Parse(date);
}
