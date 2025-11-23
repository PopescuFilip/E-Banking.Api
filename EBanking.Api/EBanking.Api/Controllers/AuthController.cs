using EBanking.Api.DTOs;
using EBanking.Api.Security;
using EBanking.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace EBanking.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(IUserService _userService, IJwtTokenGenerator _jwtTokenGenerator) : Controller
{
    private const string InvalidLoginMessage = "Invalid email or password";

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginDto user)
    {
        var (email, password) = user;

        if (!_userService.Exists(email, password))
            return BadRequest(InvalidLoginMessage);

        var token = _jwtTokenGenerator.GenerateToken(email);
            return Ok(token);
    }
}