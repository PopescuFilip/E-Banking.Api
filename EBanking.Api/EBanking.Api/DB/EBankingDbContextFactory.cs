using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EBanking.Api.DB;

public class EBankingDbContextFactory : IDesignTimeDbContextFactory<EBankingDbContext>
{
    private const string EBankingDb = "EBankingDb";

    private readonly DbContextOptions<EBankingDbContext> _options = new DbContextOptionsBuilder<EBankingDbContext>()
        .UseSqlServer(new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build()
            .GetConnectionString(EBankingDb))
        .Options;

    public EBankingDbContext CreateDbContext(string[] args) => new(_options);
}