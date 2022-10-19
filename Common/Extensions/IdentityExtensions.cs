using System.Security.Claims;
using System.Security.Principal;

namespace Common.Extensions;

public static class IdentityExtensions
{
    public static Guid? GetUserId(this IIdentity identity)
    {
        var claimsIdentity = identity as ClaimsIdentity;
        Claim claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

        if (!Guid.TryParse(claim?.Value, out Guid userId))
        {
            return null;
        }

        return userId;
    }
}