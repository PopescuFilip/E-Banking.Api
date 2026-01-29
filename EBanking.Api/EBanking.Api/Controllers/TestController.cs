using Microsoft.AspNetCore.Mvc;

namespace EBanking.Api.Controllers;

[ApiController]
[Route("api/test")]
public class TestController : Controller
{
    [HttpGet]
    public string Get() => "test";
}