using System.ComponentModel.DataAnnotations;

namespace EBanking.Api.DB.Models;

public class User
{
    [Key]
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public User(string name, string phoneNumber, string email, string password) =>
        (Name, PhoneNumber, Email, Password) = (name, phoneNumber, email, password);
}