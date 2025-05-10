namespace SmartClinic.API.RequestHelper;

public static class RequestExtensions
{

    public static int GetUserId(this ClaimsPrincipal User)
        => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
}
