using EBanking.Api.DB;
using EBanking.Api.DB.Models;
using EBanking.Api.DTOs.Payment;

namespace EBanking.Api.Services;

public interface IPaymentService
{
    bool MakePayment(OneTimePaymentOptions options);

    bool MakePayment(RecurringPaymentOptions options);
}

public class PaymentService(EBankingDbContext _dbContext) : IPaymentService
{
    public bool MakePayment(OneTimePaymentOptions options)
    {
        var senderAccount = _dbContext.Accounts.SingleOrDefault(a => a.Iban == options.FromIban);
        if (senderAccount == null)
            return false;

        if (senderAccount.Balance < options.Amount)
            return false;

        var receiverAccount = _dbContext.Accounts.SingleOrDefault(a => a.Iban == options.ToIban);
        if (receiverAccount == null)
            return false;

        senderAccount.Balance -= options.Amount;
        receiverAccount.Balance += options.Amount;
        _dbContext.Accounts.Update(senderAccount);
        _dbContext.Accounts.Update(receiverAccount);

        var newTransaction = options.ToTransaction();
        _dbContext.Transactions.Add(newTransaction);

        _dbContext.SaveChanges();
        return true;
    }

    public bool MakePayment(RecurringPaymentOptions options)
    {
        var userId = _dbContext.Users
            .Where(u => u.Account.Iban == options.FromIban)
            .Select(u => new { UserId = u.Id })
            .FirstOrDefault();

        if (userId == null)
            return false;

        var lastMadePayment = DateTime.MinValue;
        if (MakePayment(options as OneTimePaymentOptions))
            lastMadePayment = DateTime.Now;

        var recurringPayment = new RecurringPaymentDefinition(
            senderIban: options.FromIban,
            receiverIban: options.ToIban,
            amount: options.Amount,
            recurrency: options.Recurrency,
            lastMadePayment: lastMadePayment,
            userId: userId.UserId,
            receiverAccountName: options.ToAccountName,
            details: options.Details);

        _dbContext.PaymentDefinitions.Add(recurringPayment);
        _dbContext.SaveChanges();
        return true;
    }
}