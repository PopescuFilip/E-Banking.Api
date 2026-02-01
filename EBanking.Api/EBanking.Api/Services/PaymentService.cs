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

        var newTransaction = Transaction.CreateNew(
            SenderIban: options.FromIban,
            ReceiverIban: options.ToIban,
            ReceiverAccountName: options.ToAccountName,
            Amount: options.Amount,
            Details: options.Details
            );
        _dbContext.Transactions.Add(newTransaction);

        _dbContext.SaveChanges();
        return true;
    }

    public bool MakePayment(RecurringPaymentOptions options)
    {
        throw new NotImplementedException();
    }
}