using System.IdentityModel.Tokens.Jwt;
using SmartClinic.Application.Models;

namespace SmartClinic.API.RequestHelper;

public static class RequestExtensions
{

    public static int GetUserId(this ClaimsPrincipal User)
        => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    public static ReceiverData GetUserData(this ClaimsPrincipal User)
       => new(GetUserId(User)
           , User.FindFirstValue(JwtRegisteredClaimNames.Name)!
           , User.FindFirstValue(JwtRegisteredClaimNames.Email)!);
}
