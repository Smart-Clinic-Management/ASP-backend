namespace SmartClinic.API.RequestHelper;

public static class RequestExtensions
{

    public static int GetUserId(this ClaimsPrincipal User)
        => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    public static MailData GetUserData(this ClaimsPrincipal User)
       => new(GetUserId(User)
           , User.FindFirstValue(JwtRegisteredClaimNames.Name)!
           , User.FindFirstValue(ClaimTypes.Email)!);
}
