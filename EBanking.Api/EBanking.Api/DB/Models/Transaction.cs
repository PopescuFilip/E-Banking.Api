using System.ComponentModel.DataAnnotations;

namespace EBanking.Api.DB.Models;

public record Transaction(
    string SenderIban,
    string ReceiverIban,
    string ReceiverAccountName,
    decimal Amount,
    string Details,
    DateTime CreatedTime)
{
    [Key]
    public int Id { get; private set; }

    public static Transaction CreateNew(
        string SenderIban,
        string ReceiverIban,
        string ReceiverAccountName,
        decimal Amount,
        string Details) =>
        new(
            SenderIban: SenderIban,
            ReceiverIban: ReceiverIban,
            ReceiverAccountName: ReceiverAccountName,
            Amount: Amount,
            Details: Details,
            DateTime.Now);
}