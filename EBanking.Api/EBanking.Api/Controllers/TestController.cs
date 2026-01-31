using EBanking.Api.DB;
using EBanking.Api.DB.Models;
using EBanking.Api.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EBanking.Api.Controllers;

[ApiController]
[Route("api/test")]
public class TestController(IDbContextFactory<EBankingDbContext> _dbContextFactory) : Controller
{
    [HttpGet]
    public string Get() => "test";

    [Authorize]
    [HttpGet("users")]
    public IEnumerable<User> GetUsers()
    {
        var subject = HttpContext.GetSubjectClaimValue();

        using var dbContext = _dbContextFactory.CreateDbContext();

        var allUsers = dbContext.Users.ToList();

        return allUsers;
    }
}