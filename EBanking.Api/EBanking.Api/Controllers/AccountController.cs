using EBanking.Api.DB;
using EBanking.Api.DTOs;
using EBanking.Api.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EBanking.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/account")]
public class AccountController(EBankingDbContext _dbContext) : ControllerBase
{
    [HttpGet("account-details")]
    public IActionResult GetAccountDetails()
    {
        var senderEmail = HttpContext.GetSubjectClaimValue();
        var accountDetails = _dbContext.Users
            .Where(x => x.Email == senderEmail)
            .Select(x => new AccountDetails(x.Account.Iban, x.Account.Balance))
            .FirstOrDefault();

        if (accountDetails == null)
            return NotFound($"No account found for email {senderEmail}");

        return Ok(accountDetails);
    }
}