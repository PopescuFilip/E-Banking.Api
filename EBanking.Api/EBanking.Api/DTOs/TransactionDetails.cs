namespace EBanking.Api.DTOs;

public record TransactionDetails(string SenderIban, string ReceiverIban, decimal Amount);

public record CreateTransactionOptions(string FromIban, string ToAccountName, string ToIban, decimal Amount, string Details);