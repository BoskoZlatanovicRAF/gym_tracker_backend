using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace GymTracker_backend.Helpers;

public static class ClaimsPrincipalExtension
{
    public static Guid GetUserId(this ClaimsPrincipal user)
    {
        var id = user.FindFirstValue(JwtRegisteredClaimNames.Sub)
                 ?? user.FindFirstValue(ClaimTypes.NameIdentifier)
                 ?? throw new InvalidOperationException("No user id claim found");

        return Guid.Parse(id);
    }
}