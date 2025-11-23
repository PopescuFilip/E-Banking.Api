using EBanking.Api.DB;
using EBanking.Api.DB.Models;
using Microsoft.EntityFrameworkCore;

namespace EBanking.Api.Services;

public class UserService(IDbContextFactory<EBankingDbContext> _dbContextFactory) : IUserService
{
    public bool Exists(string email)
    {
        using var dbContext = _dbContextFactory.CreateDbContext();

        return dbContext.Users.Any(u => u.Email == email);
    }

    public bool Exists(string email, string password)
    {
        using var dbContext = _dbContextFactory.CreateDbContext();

        return dbContext.Users.Any(u => u.Email == email && u.Password == password);
    }

    public bool Create(string email, string password)
    {
        if (Exists(email))
            return false;

        using var dbContext = _dbContextFactory.CreateDbContext();

        var user = new User(email, password);

        dbContext.Users.Add(user);
        dbContext.SaveChanges();
        return true;
    }
}