using EBanking.Api.DB;
using EBanking.Api.DTOs;
using EBanking.Api.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EBanking.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/user")]
public class UserController(EBankingDbContext _dbContext) : ControllerBase
{
    [HttpGet("user-details")]
    public IActionResult GetUserDetails()
    {
        var senderEmail = HttpContext.GetSubjectClaimValue();
        var userDetails = _dbContext.Users
            .Where(x => x.Email == senderEmail)
            .Select(x => new UserDetails(x.Name, x.PhoneNumber, x.Email, x.Password))
            .SingleOrDefault();

        if (userDetails is null)
            return NotFound($"User details not found for email {senderEmail}");

        return Ok(userDetails);
    }

    [HttpPut("user-details")]
    public IActionResult UpdateUserDetails([FromBody] UpdateUserDetailsRequest request)
    {
        var senderEmail = HttpContext.GetSubjectClaimValue();

        var user = _dbContext.Users.SingleOrDefault(x => x.Email == senderEmail);

        if (user is null)
            return NotFound($"User not found for email {senderEmail}");

        user.PhoneNumber = request.PhoneNumber;
        user.Name = request.Name;
        user.Password = request.Password;
        _dbContext.Users.Update(user);
        _dbContext.SaveChanges();

        return Ok();
    }
}