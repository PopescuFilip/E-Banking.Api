namespace EBanking.Api.DTOs;

public record RecurringPaymentDto(string ReceiverIban, string ReceiverAccountName, DateTime NextPayment, string Recurrency, decimal Amount);