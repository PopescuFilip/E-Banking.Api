using EBanking.Api.DB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EBanking.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/users")]
public class UserController(IDbContextFactory<EBankingDbContext> _dbContextFactory) : ControllerBase
{

}