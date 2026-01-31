using EBanking.Api.DTOs;
using EBanking.Api.Security;
using EBanking.Api.Services;
using EBanking.Api.Validators;
using Microsoft.AspNetCore.Mvc;

namespace EBanking.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(
    IUserService _userService,
    IJwtTokenGenerator _jwtTokenGenerator,
    IEmailValidator _emailValidator)
    : Controller
{
    private const string InvalidLoginMessage = "Invalid email or password";
    private const string DuplicateEmailMessage = "Email is already used by different account";
    private const string InvalidEmailFormat = "\'{0}\' is an invalid email";

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginDto loginInfo)
    {
        var (email, password) = loginInfo;

        if (!_userService.Exists(email, password))
            return Unauthorized(InvalidLoginMessage);

        var token = _jwtTokenGenerator.GenerateToken(email);
            return Ok(token);
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody] RegisterDto registerInfo)
    {
        var (name, phoneNumber, email, password) = registerInfo;

        if (!_emailValidator.IsValid(email))
            return BadRequest(string.Format(InvalidEmailFormat, email));

        if (_userService.Exists(email))
            return BadRequest(DuplicateEmailMessage);

        if (!_userService.Create(name, phoneNumber, email, password))
            return BadRequest();

        return Ok();
    }
}