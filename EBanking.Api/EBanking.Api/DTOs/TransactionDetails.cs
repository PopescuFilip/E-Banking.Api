namespace EBanking.Api.DTOs;

public record TransactionDetails(string SenderIban, string ReceiverIban, decimal Amount);