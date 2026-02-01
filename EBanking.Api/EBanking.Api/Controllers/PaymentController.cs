using EBanking.Api.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EBanking.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/payment")]
public class PaymentController : ControllerBase
{
    [HttpPost("one-time")]
    public IActionResult MakeOneTimePayment([FromBody] OneTimePaymentRequest request)
    {
        return Ok();
    }
}