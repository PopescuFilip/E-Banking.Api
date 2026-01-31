using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace EBanking.Api.Security;

public static class JwtDefaults
{
    private const string Secret = "my1very1secure1and1impossible1to1guess1secret";

    public const string Algorithm = SecurityAlgorithms.HmacSha256;

    public static readonly SecurityKey Key;

    static JwtDefaults()
    {
        var secretBytes = Encoding.UTF8.GetBytes(Secret);
        Key = new SymmetricSecurityKey(secretBytes);
    }
}