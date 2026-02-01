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

    [HttpGet("transactions")]
    public IActionResult GetTransactionsForAccount()
    {
        var senderEmail = HttpContext.GetSubjectClaimValue();

        var accountIban = _dbContext.Users
            .Where(x => x.Email == senderEmail)
            .Select(x => x.Account.Iban)
            .FirstOrDefault();

        if (accountIban == null)
            return NotFound($"No account found for email {senderEmail}");

        var transactions = _dbContext.Transactions
            .Where(t => t.SenderIban == accountIban || t.ReceiverIban == accountIban)
            .Select(t => new TransactionDetails(t.SenderIban, t.ReceiverIban, t.Amount))
            .ToList();

        return Ok(transactions);
    }
}