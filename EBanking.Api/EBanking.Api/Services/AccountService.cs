using EBanking.Api.DB;
using EBanking.Api.DB.Models;

namespace EBanking.Api.Services;

public interface IAccountService
{
    Account CreateAccount(string ownerEmail);
}

public class AccountService(
    EBankingDbContext _dbContext,
    IIbanGenerator ibanGenerator)
    : IAccountService
{
    public Account CreateAccount(string ownerEmail)
    {
        var iban = ibanGenerator.Generate(ownerEmail);
        var account = new Account(iban);

        var addedAccount = _dbContext.Accounts.Add(account).Entity;
        _dbContext.SaveChanges();
        return addedAccount;
    }
}