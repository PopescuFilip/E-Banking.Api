namespace EBanking.Api.DTOs;

public record LoginDto(string Email, string Password);

public record RegisterDto(string Name, string PhoneNumber, string Email, string Password);