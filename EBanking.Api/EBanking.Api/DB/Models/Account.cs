using System.ComponentModel.DataAnnotations;

namespace EBanking.Api.DB.Models;

public class Account
{
    [Key]
    public int Id { get; init; }

    public string Iban { get; private set; } = null!;

    public decimal Balance { get; set; } = 0;

    public Account(string iban) => Iban = iban;
}