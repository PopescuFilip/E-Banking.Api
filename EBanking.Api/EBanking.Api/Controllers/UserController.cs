using EBanking.Api.DB;
using EBanking.Api.DB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EBanking.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/users")]
public class UserController(IDbContextFactory<EBankingDbContext> _dbContextFactory) : ControllerBase
{
    [HttpGet(Name = "GetUsers")]
    public IEnumerable<User> Get()
    {
        using var dbContext = _dbContextFactory.CreateDbContext();

        var allUsers = dbContext.Users.ToList();

        return allUsers;
    }
}