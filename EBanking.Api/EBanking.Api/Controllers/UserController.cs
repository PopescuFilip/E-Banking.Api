using EBanking.Api.DB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EBanking.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/users")]
public class UserController(EBankingDbContext dbContext) : ControllerBase
{

}