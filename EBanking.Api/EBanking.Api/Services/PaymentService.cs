using EBanking.Api.DB;
using EBanking.Api.DB.Models;
using EBanking.Api.DTOs;

namespace EBanking.Api.Services;

public interface IPaymentService
{
    bool MakePayment(CreateTransactionOptions transactionOptions);

    //bool MakePayment(OneTimePaymentRequest paymentRequest);
}

public class PaymentService(EBankingDbContext _dbContext) : IPaymentService
{
    public bool MakePayment(CreateTransactionOptions transactionOptions)
    {
        var senderAccount = _dbContext.Accounts.SingleOrDefault(a => a.Iban == transactionOptions.FromIban);
        if (senderAccount == null)
            return false;

        if (senderAccount.Balance < transactionOptions.Amount)
            return false;

        var receiverAccount = _dbContext.Accounts.SingleOrDefault(a => a.Iban == transactionOptions.ToIban);
        if (receiverAccount == null)
            return false;

        senderAccount.Balance -= transactionOptions.Amount;
        receiverAccount.Balance += transactionOptions.Amount;
        _dbContext.Accounts.Update(senderAccount);
        _dbContext.Accounts.Update(receiverAccount);

        var newTransaction = Transaction.CreateNew(
            SenderIban: transactionOptions.FromIban,
            ReceiverIban: transactionOptions.ToIban,
            ReceiverAccountName: transactionOptions.ToAccountName,
            Amount: transactionOptions.Amount,
            Details: transactionOptions.Details
            );
        _dbContext.Transactions.Add(newTransaction);

        _dbContext.SaveChanges();
        return true;
    }
}