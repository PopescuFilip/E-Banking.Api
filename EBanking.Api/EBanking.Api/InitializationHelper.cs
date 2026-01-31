using EBanking.Api.DB;
using EBanking.Api.DB.Models;
using EBanking.Api.Services;
using Microsoft.EntityFrameworkCore;

namespace EBanking.Api;

public class InitializationHelper(IServiceProvider serviceProvider)
{
    public void MigrateAndInitializeDb()
    {
        var dbContext = serviceProvider.GetRequiredService<EBankingDbContext>();
        dbContext.Database.Migrate();

        if (!dbContext.Users.Any())
        {
            var email = "admin@gmail.com";
            var account = serviceProvider.GetRequiredService<IAccountService>().CreateAccount(email);

            var admin = new User(
                name: "admin",
                phoneNumber: "1234567890",
                email: email,
                password: "pass",
                accountId: account.Id);

            dbContext.Users.Add(admin);
            dbContext.SaveChanges();
        }
    }
}