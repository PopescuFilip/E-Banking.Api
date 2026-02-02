
using EBanking.Api.DB;
using EBanking.Api.DB.Models;
using EBanking.Api.DTOs.Payment;

namespace EBanking.Api.Services;

public class RecurringPaymentHostedService(IServiceProvider _serviceProvider) : IHostedService
{
    private static readonly TimeSpan IntervalBetweenChecks = TimeSpan.FromHours(1);

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            using var scope = _serviceProvider.CreateAsyncScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<EBankingDbContext>();

            var paymentDefinitions = dbContext.PaymentDefinitions
                .AsEnumerable()
                .Where(p => p.GetNextPaymentDate() <= DateTime.Now)
                .ToList();
            foreach (var paymentDefinition in paymentDefinitions)
                ApplyPayment(paymentDefinition);

            await Task.Delay(IntervalBetweenChecks, cancellationToken);
        }
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        //
    }

    private void ApplyPayment(RecurringPaymentDefinition paymentDefinition)
    {
        using var scope = _serviceProvider.CreateAsyncScope();
        var paymentService = scope.ServiceProvider.GetRequiredService<IPaymentService>();
        var options = paymentDefinition.ToPaymentOptions();

        var success = paymentService.MakePayment(options);
        if (!success)
            return;

        var dbContext = scope.ServiceProvider.GetRequiredService<EBankingDbContext>();
        dbContext.Attach(paymentDefinition);
        paymentDefinition.LastMadePayment = DateTime.Now;
        dbContext.Update(paymentDefinition);
        dbContext.SaveChanges();
    }
}