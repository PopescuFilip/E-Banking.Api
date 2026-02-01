using EBanking.Api.DB;
using EBanking.Api.DB.Models;

namespace EBanking.Api.Services;

public interface IUserService
{
    bool Exists(string email);
    bool Exists(string email, string password);
    bool Create(string name, string phoneNumber, string email, string password, int accountId);
}

public class UserService(EBankingDbContext _dbContext) : IUserService
{
    public bool Exists(string email)
    {
        return _dbContext.Users.Any(u => u.Email == email);
    }

    public bool Exists(string email, string password)
    {
        return _dbContext.Users.Any(u => u.Email == email && u.Password == password);
    }

    public bool Create(string name, string phoneNumber, string email, string password, int accountId)
    {
        if (_dbContext.Accounts.Find(accountId) is null)
            return false;

        if (Exists(email))
            return false;

        var user = new User(name, phoneNumber, email, password, accountId);

        _dbContext.Users.Add(user);
        _dbContext.SaveChanges();
        return true;
    }
}