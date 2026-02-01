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

            var email2 = "filip@gmail.com";
            var account2 = serviceProvider.GetRequiredService<IAccountService>().CreateAccount(email2);

            var otherUser = new User(
                name: "Popescu Filip",
                phoneNumber: "0742444222",
                email: email2,
                password: "pass",
                accountId: account2.Id);

            dbContext.Users.Add(admin);
            dbContext.Users.Add(otherUser);
            dbContext.SaveChanges();
        }
    }
}