using EBanking.Api.DB;
using EBanking.Api.DB.Models;

namespace EBanking.Api.Services;

public interface IAccountService
{
    Account CreateAccount(string ownerEmail);
    bool Exists(string iban);
    bool IsAccountOwner(string ownerEmail, string iban);
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

    public bool Exists(string iban)
    {
        return _dbContext.Accounts.Where(a => a.Iban == iban).Any();
    }

    public bool IsAccountOwner(string ownerEmail, string iban)
    {
        var accountForOwner = _dbContext.Users
            .Where(u => u.Email == ownerEmail)
            .Select(x => new { x.Account.Iban })
            .SingleOrDefault();

        return accountForOwner is not null && accountForOwner.Iban == iban;
    }
}