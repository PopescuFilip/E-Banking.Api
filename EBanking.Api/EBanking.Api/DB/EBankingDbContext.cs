using EBanking.Api.DB.Models;
using Microsoft.EntityFrameworkCore;

namespace EBanking.Api.DB;

public class EBankingDbContext(DbContextOptions<EBankingDbContext> dbContextOptions) : DbContext(dbContextOptions)
{
    public DbSet<User> Users { get; set; }

    public DbSet<Account> Accounts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>()
            .Property(x => x.Balance)
            .HasPrecision(18, 2);

        base.OnModelCreating(modelBuilder);
    }
}