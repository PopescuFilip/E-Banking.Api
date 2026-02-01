using System.ComponentModel.DataAnnotations;

namespace EBanking.Api.DB.Models;

public class User
{
    [Key]
    public Guid Id { get; init; }

    public string Name { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int AccountId { get; private set; }

    public Account Account { get; private set; } = null!;

    public User(string name, string phoneNumber, string email, string password, int accountId) =>
        (Name, PhoneNumber, Email, Password, AccountId) = (name, phoneNumber, email, password, accountId);
}