using EBanking.Api.DTOs;
using EBanking.Api.Security;
using Microsoft.AspNetCore.Mvc;

namespace EBanking.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(IJwtTokenGenerator _jwtTokenGenerator) : Controller
{
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginDto user)
    {
        if (user.Username == "admin" && user.Password == "password")
        {
            var token = _jwtTokenGenerator.GenerateToken(user.Username);
            return Ok(token);
        }
        return Unauthorized();
    }
}