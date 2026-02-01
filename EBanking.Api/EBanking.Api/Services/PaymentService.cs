using EBanking.Api.DB;
using EBanking.Api.DB.Models;
using EBanking.Api.DTOs;
using Microsoft.EntityFrameworkCore;

namespace EBanking.Api.Services;

public interface IPaymentService
{
    bool MakePayment(OneTimePaymentRequest paymentRequest);
}

public class PaymentService(EBankingDbContext _dbContext) : IPaymentService
{
    public bool MakePayment(OneTimePaymentRequest paymentRequest)
    {
        var senderAccount = _dbContext.Accounts.SingleOrDefault(a => a.Iban == paymentRequest.FromIban);
        if (senderAccount == null)
            return false;

        if (senderAccount.Balance < paymentRequest.Amount)
            return false;

        var receiverAccount = _dbContext.Accounts.SingleOrDefault(a => a.Iban == paymentRequest.ToIban);
        if (receiverAccount == null)
            return false;

        senderAccount.Balance -= paymentRequest.Amount;
        receiverAccount.Balance += paymentRequest.Amount;
        _dbContext.Accounts.Update(senderAccount);
        _dbContext.Accounts.Update(receiverAccount);

        var newTransaction = new Transaction(
            SenderIban: paymentRequest.FromIban,
            ReceiverIban: paymentRequest.ToIban,
            ReceiverAccountName: paymentRequest.ToAccountName,
            Amount: paymentRequest.Amount,
            Details: paymentRequest.Details
            );
        _dbContext.Transactions.Add(newTransaction);

        _dbContext.SaveChanges();
        return true;
    }
}