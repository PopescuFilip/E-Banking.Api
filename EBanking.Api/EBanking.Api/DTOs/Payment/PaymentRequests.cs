using EBanking.Api.DB.Models;
using System.Diagnostics.CodeAnalysis;

namespace EBanking.Api.DTOs.Payment;

public record OneTimePaymentRequest(string FromIban, string ToAccountName, string ToIban, decimal Amount, string Details);

public record RecurringPaymentRequest(string FromIban, string ToIban, string Recurrency, decimal Amount);

public static class PaymentRequestExtensions
{
    public static bool TryParseRecurrency(this string recurrencyString, [MaybeNullWhen(false)][NotNullWhen(true)] out Recurrency? recurency)
    {
        recurency = null;

        if (Enum.TryParse<Recurrency>(recurrencyString, ignoreCase: true, out var parsedRecurrency))
        {
            recurency = parsedRecurrency;
            return true;
        }

        return false;
    }

    public static OneTimePaymentOptions ToCreateTransactionOptions(this OneTimePaymentRequest request) =>
        new(
            FromIban: request.FromIban,
            ToAccountName: request.ToAccountName,
            ToIban: request.ToIban,
            Amount: request.Amount,
            Details: request.Details);
}