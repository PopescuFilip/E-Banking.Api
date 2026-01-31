using EBanking.Api.DB;
using EBanking.Api.DB.Models;
using EBanking.Api.DTOs;
using EBanking.Api.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EBanking.Api.Controllers;

[ApiController]
[Route("api/test")]
public class TestController(EBankingDbContext _dbContext) : Controller
{
    [HttpGet]
    public string Get() => "test";

    [Authorize]
    [HttpGet("users")]
    public IEnumerable<User> GetUsers()
    {
        var subject = HttpContext.GetSubjectClaimValue();

        var allUsers = _dbContext.Users.ToList();

        return allUsers;
    }

    [HttpGet("accounts")]
    public IEnumerable<AccountDto> GetAccount()
    {
        return _dbContext.Accounts
            .Select(a => new AccountDto(a.Iban, a.Balance))
            .ToList();
    }
}