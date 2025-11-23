using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace EBanking.Api.Security;

public static class JwtDefaults
{
    private const string Secret = "my1very1secure1and1impossible1to1guess1secret";

    public const string Algorithm = SecurityAlgorithms.HmacSha256;

    private static readonly byte[] _secretBytes;

    public static SecurityKey Key => new SymmetricSecurityKey(_secretBytes);

    static JwtDefaults() => _secretBytes = Encoding.UTF8.GetBytes(Secret);
}