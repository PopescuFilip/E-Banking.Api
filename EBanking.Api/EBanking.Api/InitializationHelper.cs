using EBanking.Api.DB;
using EBanking.Api.DB.Models;
using Microsoft.EntityFrameworkCore;

namespace EBanking.Api;

public class InitializationHelper(EBankingDbContext _dbContext)
{
    public void MigrateAndInitializeDb()
    {
        _dbContext.Database.Migrate();

        if (!_dbContext.Users.Any())
        {
            var admin = new User(email: "admin@gmail.com", password: "pass");

            _dbContext.Users.Add(admin);
            _dbContext.SaveChanges();
        }
    }
}