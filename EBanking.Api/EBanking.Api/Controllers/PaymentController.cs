using EBanking.Api.DTOs;
using EBanking.Api.Security;
using EBanking.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EBanking.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/payment")]
public class PaymentController(IAccountService _accountService, IPaymentService _paymentService) : ControllerBase
{
    [HttpPost("one-time")]
    public IActionResult MakeOneTimePayment([FromBody] OneTimePaymentRequest request)
    {
        var subject = HttpContext.GetSubjectClaimValue();

        if (_accountService.Exists(request.FromIban))
            return NotFound($"Account {request.FromIban} does not exist");

        if (_accountService.Exists(request.ToIban))
            return NotFound($"Account {request.ToIban} does not exist");

        if (_accountService.IsAccountOwner(subject, request.FromIban))
            return Unauthorized($"You do not have access to account {request.FromIban}");

        if (!_paymentService.MakePayment(request))
            return BadRequest("Insufficient balance");

        return Ok();
    }
}