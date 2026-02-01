namespace EBanking.Api.DTOs;

public record OneTimePaymentRequest(string FromIban, string ToAccountName, string ToIban, decimal Amount, string Details);