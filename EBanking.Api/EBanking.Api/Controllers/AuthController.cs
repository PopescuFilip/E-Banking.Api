using EBanking.Api.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static EBanking.Api.Constants;

namespace EBanking.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : Controller
{
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginDto user)
    {
        if (user.Username == "admin" && user.Password == "password")
        {
            var token = GenerateJwtToken(user.Username);
            return Ok(token);
        }
        return Unauthorized();
    }

    private static string GenerateJwtToken(string username)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "yourdomain.com",
            audience: "yourdomain.com",
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}