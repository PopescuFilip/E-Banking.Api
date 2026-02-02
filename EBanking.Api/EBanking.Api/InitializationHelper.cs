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

            var email3 = "john.doe@gmail.com";
            var account3 = serviceProvider.GetRequiredService<IAccountService>().CreateAccount(email3);

            var johnDoe = new User(
                name: "John Doe",
                phoneNumber: "0744399888",
                email: email3,
                password: "pass",
                accountId: account3.Id);

            var email4 = "john.smith@gmail.com";
            var account4 = serviceProvider.GetRequiredService<IAccountService>().CreateAccount(email4);
            var landlord = new User(
                name: "John Smith",
                phoneNumber: "0722299888",
                email: email4,
                password: "pass",
                accountId: account4.Id);

            var email5 = "netflix.company@gmail.com";
            var account5 = serviceProvider.GetRequiredService<IAccountService>().CreateAccount(email4);
            var waterCompany = new User(
                name: "Netflix",
                phoneNumber: "0799999888",
                email: email5,
                password: "pass",
                accountId: account5.Id);

            account.Balance = 3566.43M;
            account2.Balance = 1022.30M;
            account3.Balance = 200M;
            account4.Balance = 2M;

            dbContext.Accounts.Update(account);
            dbContext.Accounts.Update(account2);
            dbContext.Accounts.Update(account3);
            dbContext.Accounts.Update(account4);

            var addedAdmin = dbContext.Users.Add(admin).Entity;
            var addedUser = dbContext.Users.Add(otherUser).Entity;
            dbContext.Users.Add(johnDoe);
            dbContext.Users.Add(landlord);
            dbContext.Users.Add(waterCompany);
            dbContext.SaveChanges();

            var transaction1 = Transaction.CreateNew(account.Iban, account2.Iban, "Filip", 30, "Food");
            var transaction2 = Transaction.CreateNew(account3.Iban, account2.Iban, "Filip", 100, "Gift");
            var transaction3 = Transaction.CreateNew(account2.Iban, account3.Iban, "John Doe", 42.33M, "Pizza");
            dbContext.Transactions.Add(transaction1);
            dbContext.Transactions.Add(transaction2);
            dbContext.Transactions.Add(transaction3);
            var recurringPaymentDefinitions1 = new RecurringPaymentDefinition(
                account2.Iban,
                account4.Iban,
                1000,
                Recurrency.Monthly,
                DateTime.Now.AddDays(-20),
                addedUser.Id,
                "Landlord",
                "Rent");
            var recurringPaymentDefinitions2 = new RecurringPaymentDefinition(
                account2.Iban,
                account5.Iban,
                50,
                Recurrency.Monthly,
                DateTime.Now.AddDays(-40),
                addedUser.Id,
                "Netflix",
                "subscription");

            var testDefinition1 = new RecurringPaymentDefinition(
                account.Iban,
                account3.Iban,
                5,
                Recurrency.Daily,
                DateTime.Now.AddDays(-1),
                addedAdmin.Id,
                "sth",
                "sth");

            var testDefinition2 = new RecurringPaymentDefinition(account.Iban, account3.Iban, 5,
                Recurrency.Weekly,
                DateTime.Now.AddDays(-8), addedAdmin.Id, "sth", "sth");
            var testDefinition3 = new RecurringPaymentDefinition(account.Iban, account3.Iban, 5,
                Recurrency.Yearly,
                DateTime.Now.AddMonths(-13), addedAdmin.Id, "sth", "sth");

            dbContext.PaymentDefinitions.Add(recurringPaymentDefinitions1);
            dbContext.PaymentDefinitions.Add(recurringPaymentDefinitions2);
            dbContext.PaymentDefinitions.Add(testDefinition1);
            dbContext.PaymentDefinitions.Add(testDefinition2);
            dbContext.PaymentDefinitions.Add(testDefinition3);

            dbContext.SaveChanges();
        }
    }
}