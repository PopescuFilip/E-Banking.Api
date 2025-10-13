using EBanking.Api.DB.Models;
using Microsoft.EntityFrameworkCore;

namespace EBanking.Api.DB;

public class EBankingDbContext(DbContextOptions<EBankingDbContext> dbContextOptions) : DbContext(dbContextOptions)
{
    public DbSet<User> Users { get; set; }
}