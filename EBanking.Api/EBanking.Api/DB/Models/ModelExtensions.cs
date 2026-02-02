namespace EBanking.Api.DB.Models;

public static class ModelExtensions
{
    public static DateTime GetNextPaymentDate(this RecurringPaymentDefinition paymentDefinition) => paymentDefinition.Recurrency switch
    {
        Recurrency.Daily => paymentDefinition.LastMadePayment.AddDays(1),
        Recurrency.Weekly => paymentDefinition.LastMadePayment.AddDays(7),
        Recurrency.Monthly => paymentDefinition.LastMadePayment.AddMonths(1),
        Recurrency.Yearly => paymentDefinition.LastMadePayment.AddYears(1),
        _ => throw new NotSupportedException()
    };
}