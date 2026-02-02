using EBanking.Api.DB.Models;

namespace EBanking.Api.DTOs.Payment;

public record OneTimePaymentOptions(string FromIban, string ToAccountName, string ToIban, decimal Amount, string Details);

public record RecurringPaymentOptions(
    string FromIban, string ToAccountName, string ToIban, decimal Amount, string Details, Recurrency Recurrency)
    : OneTimePaymentOptions(FromIban, ToAccountName, ToIban, Amount, Details);

public static class PaymentOptionsExtensions
{
    public static OneTimePaymentOptions ToPaymentOptions(this RecurringPaymentDefinition recurringPayment) =>
        new(
            FromIban: recurringPayment.SenderIban,
            ToAccountName: recurringPayment.ReceiverAccountName,
            ToIban: recurringPayment.ReceiverIban,
            Amount: recurringPayment.Amount,
            Details: recurringPayment.Details);

    public static Transaction ToTransaction(this OneTimePaymentOptions options) =>
        Transaction.CreateNew(
            SenderIban: options.FromIban,
            ReceiverIban: options.ToIban,
            ReceiverAccountName: options.ToAccountName,
            Amount: options.Amount,
            Details: options.Details);
}