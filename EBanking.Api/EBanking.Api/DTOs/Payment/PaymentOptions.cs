using EBanking.Api.DB.Models;

namespace EBanking.Api.DTOs.Payment;

public record OneTimePaymentOptions(string FromIban, string ToAccountName, string ToIban, decimal Amount, string Details);

public record RecurringPaymentOptions(string FromIban, string ToIban, Recurrency Recurrency, decimal Amount);