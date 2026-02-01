namespace EBanking.Api.DTOs;

public record DepositRequest(int AccountId, decimal Amount);