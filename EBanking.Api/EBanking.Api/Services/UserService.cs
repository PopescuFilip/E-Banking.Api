using EBanking.Api.DB;
using EBanking.Api.DB.Models;

namespace EBanking.Api.Services;

public interface IUserService
{
    bool Exists(string email);
    bool Exists(string email, string password);
    bool Create(string email, string password);
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

    public bool Create(string email, string password)
    {
        if (Exists(email))
            return false;

        var user = new User(email, password);

        _dbContext.Users.Add(user);
        _dbContext.SaveChanges();
        return true;
    }
}