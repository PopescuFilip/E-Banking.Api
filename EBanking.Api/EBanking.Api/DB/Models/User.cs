using System.ComponentModel.DataAnnotations;

namespace EBanking.Api.DB.Models;

public class User
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Email { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;
}