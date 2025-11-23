using EBanking.Api.DB;
using EBanking.Api.DB.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace EBanking.Api;

public class InitializationHelper(IDbContextFactory<EBankingDbContext> _dbContextFactory)
{
    public void MigrateAndInitializeDb()
    {
        using var dbContext = _dbContextFactory.CreateDbContext();

        dbContext.Database.Migrate();

        if (!dbContext.Users.Any())
        {
            var admin = new User()
            {
                Email = "admin@gmail.com",
                Password = "pass"
            };

            dbContext.Users.Add(admin);
            dbContext.SaveChanges();
        }
    }
}