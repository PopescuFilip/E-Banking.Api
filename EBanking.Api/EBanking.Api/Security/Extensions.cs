using System.Security.Claims;

namespace EBanking.Api.Security;

public static class Extensions
{
    public static string GetNameIdClaimValue(this HttpContext httpContext) =>
        httpContext.User.Claims
            .Single(c => c.Type == ClaimTypes.NameIdentifier)
            .Value;
}