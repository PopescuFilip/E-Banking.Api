using EBanking.Api.DB;
using EBanking.Api.DB.Models;
using EBanking.Api.DTOs;
using EBanking.Api.DTOs.Payment;
using EBanking.Api.Security;
using EBanking.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EBanking.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/payment")]
public class PaymentController(
    EBankingDbContext _dbContext,
    IAccountService _accountService,
    IPaymentService _paymentService) : ControllerBase
{
    [HttpPost("one-time")]
    public IActionResult MakeOneTimePayment([FromBody] OneTimePaymentRequest request)
    {
        var subject = HttpContext.GetSubjectClaimValue();

        if (!_accountService.Exists(request.FromIban))
            return NotFound($"Account {request.FromIban} does not exist");

        if (!_accountService.Exists(request.ToIban))
            return NotFound($"Account {request.ToIban} does not exist");

        if (!_accountService.IsAccountOwner(subject, request.FromIban))
            return Unauthorized($"You do not have access to account {request.FromIban}");

        if (!_paymentService.MakePayment(request.ToCreateTransactionOptions()))
            return BadRequest("Insufficient balance");

        return Ok();
    }

    [HttpPost("recurring")]
    public IActionResult MakeRecurringPayment([FromBody] RecurringPaymentRequest request)
    {
        var subject = HttpContext.GetSubjectClaimValue();

        if (!_accountService.Exists(request.FromIban))
            return NotFound($"Account {request.FromIban} does not exist");

        if (!_accountService.Exists(request.ToIban))
            return NotFound($"Account {request.ToIban} does not exist");

        if (!_accountService.IsAccountOwner(subject, request.FromIban))
            return Unauthorized($"You do not have access to account {request.FromIban}");

        if (!request.Recurrency.TryParseRecurrency(out var recurency))
            return BadRequest($"{request.Recurrency} is not a valid recurrency");

        var options = new RecurringPaymentOptions(
            FromIban: request.FromIban,
            ToIban: request.ToIban,
            ToAccountName: request.ToAccountName,
            Amount: request.Amount,
            Details: request.Details,
            Recurrency: recurency.Value);

        if (!_paymentService.MakePayment(options))
            return BadRequest("Could not create recurring payment");

        return Ok();
    }

    [HttpGet("recurring-payments")]
    public IActionResult GetRecurringPaymentDefinitions()
    {
        var subject = HttpContext.GetSubjectClaimValue();

        var userId = _dbContext.Users
            .Select(x => new { x.Id, x.Email })
            .FirstOrDefault(x => x.Email == subject);

        if (userId == null)
            return NotFound($"User not found for email {subject}");

        var recurringPaymentDefinitions = _dbContext.PaymentDefinitions
            .Where(x => x.UserId == userId.Id)
            .Select(x => new { x.Id, x.ReceiverIban, x.ReceiverAccountName, x.LastMadePayment, x.Recurrency, x.Amount })
            .AsEnumerable()
            .Select(x => new RecurringPaymentDto(
                x.Id,
                x.ReceiverIban,
                x.ReceiverAccountName,
                x.LastMadePayment.GetNextPaymentDate(x.Recurrency),
                x.Recurrency.ToString(),
                x.Amount))
            .ToList();

        return Ok(recurringPaymentDefinitions);
    }

    [HttpDelete("recurring-payment/{id}")]
    public IActionResult Delete(int id)
    {
        var subject = HttpContext.GetSubjectClaimValue();

        var userId = _dbContext.Users
            .Select(x => new { x.Id, x.Email })
            .FirstOrDefault(x => x.Email == subject);

        if (userId == null)
            return NotFound($"User not found for email {subject}");

        var recurringPayment = _dbContext.PaymentDefinitions.Find(id);
        if (recurringPayment == null)
            return NotFound($"Recurring payment with id {id} not found");

        if (recurringPayment.UserId != userId.Id)
            return Unauthorized();

        _dbContext.PaymentDefinitions.Remove(recurringPayment);
        _dbContext.SaveChanges();

        return Ok();
    }
}