using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace GymTracker_backend.Helpers;

public static class ClaimsPrincipalExtension
{
    public static Guid GetUserId(this ClaimsPrincipal user)
    {
        return Guid.Parse(user.FindFirstValue(JwtRegisteredClaimNames.Sub)!);
    }
}