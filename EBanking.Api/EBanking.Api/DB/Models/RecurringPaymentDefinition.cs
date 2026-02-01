using System.ComponentModel.DataAnnotations;

namespace EBanking.Api.DB.Models;

public enum Recurrency
{
    Daily,
    Weekly,
    Monthly,
    Yearly
}

public class RecurringPaymentDefinition(string senderIban, string receiverIban, decimal amount, Recurrency recurrency,
    DateTime lastMadePayment, Guid userId)
{
    [Key]
    public int Id { get; private set; }

    public string SenderIban { get; private set; } = senderIban;

    public string ReceiverIban { get; private set; } = receiverIban;

    public decimal Amount { get; private set; } = amount;

    public Recurrency Recurrency { get; private set; } = recurrency;

    public DateTime LastMadePayment { get; set; } = lastMadePayment;

    public Guid UserId { get; private set; } = userId;

    public User User { get; private set; } = null!;
}