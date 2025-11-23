using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using static EBanking.Api.Security.JwtDefaults;

namespace EBanking.Api.Security;

public interface IJwtTokenGenerator
{
    string GenerateToken(string username);
}

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;

    public JwtTokenGenerator() => _jwtSecurityTokenHandler = new();

    public string GenerateToken(string username)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: new SigningCredentials(Key, Algorithm));

        return _jwtSecurityTokenHandler.WriteToken(token);
    }
}