using System.Reflection;

namespace SmartClinic.Application.Bases;

public static class PagingValidator
{
    public static bool IsValidProperty(string? PropName, Type type)
    {
        if (PropName is null) return false;

        return typeof(GetAllDoctorsResponse).GetProperty(PropName, BindingFlags.IgnoreCase
               | BindingFlags.Public | BindingFlags.Instance) is not null;
    }
}
