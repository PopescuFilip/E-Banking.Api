using System.ComponentModel.DataAnnotations;

namespace EBanking.Api.DB.Models;

public record Transaction(string SenderIban, string ReceiverIban, string ReceiverAccountName, decimal Amount, string Details)
{
    [Key]
    public int Id { get; private set; }
}