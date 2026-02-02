namespace EBanking.Api.DTOs;

public record RecurringPaymentDto(int Id, string ReceiverIban, string ReceiverAccountName, DateTime NextPayment, string Recurrency, decimal Amount);