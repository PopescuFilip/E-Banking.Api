namespace EBanking.Api.DB.Models;

public static class ModelExtensions
{
    public static DateTime GetNextPaymentDate(this RecurringPaymentDefinition paymentDefinition) =>
        paymentDefinition.LastMadePayment.GetNextPaymentDate(paymentDefinition.Recurrency);

    public static DateTime GetNextPaymentDate(this DateTime lastPayment, Recurrency recurrency) => recurrency switch
    {
        Recurrency.Daily => lastPayment.AddDays(1),
        Recurrency.Weekly => lastPayment.AddDays(7),
        Recurrency.Monthly => lastPayment.AddMonths(1),
        Recurrency.Yearly => lastPayment.AddYears(1),
        _ => throw new NotSupportedException()
    };
}