using Microsoft.EntityFrameworkCore;

namespace EBanking.Api.DB;

public class EBankingDbContextFactory(IConfiguration configuration) : IDbContextFactory<EBankingDbContext>
{
    private const string EBankingDb = "EBankingDb";

    private readonly DbContextOptions<EBankingDbContext> _options = new DbContextOptionsBuilder<EBankingDbContext>()
            .UseSqlServer(configuration.GetConnectionString(EBankingDb))
            .Options;

    public EBankingDbContext CreateDbContext() => new(_options);
}