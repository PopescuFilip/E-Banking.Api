namespace EBanking.Api.DTOs;

public record AccountDto(string Email, int AccountId, string Iban, decimal Balance);

public record AccountDetails(string Iban, decimal Balance);