using System.ComponentModel.DataAnnotations;

namespace EBanking.Api.DB.Models;

public enum Recurency
{
    Daily,
    Weekly,
    Monthly,
    Yearly
}

public class RecurringPaymentDefinition(string senderIban, string receiverIban, decimal amount, Recurency recurency,
    DateTime lastMadePayment, Guid userId)
{
    [Key]
    public int Id { get; private set; }

    public string SenderIban { get; private set; } = senderIban;

    public string ReceiverIban { get; private set; } = receiverIban;

    public decimal Amount { get; private set; } = amount;

    public Recurency Recurency { get; private set; } = recurency;

    public DateTime LastMadePayment { get; set; } = lastMadePayment;

    public Guid UserId { get; private set; } = userId;

    public User User { get; private set; } = null!;
}