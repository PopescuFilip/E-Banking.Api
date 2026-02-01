
namespace EBanking.Api.Services;

public class RecurringPaymentHostedService : IHostedService
{
    private static readonly TimeSpan IntervalBetweenChecks = TimeSpan.FromHours(1);

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            Console.WriteLine("do studddddd");
            await Task.Delay(IntervalBetweenChecks, cancellationToken);
        }
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        //
    }
}