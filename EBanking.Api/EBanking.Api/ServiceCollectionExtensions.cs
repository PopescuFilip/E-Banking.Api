using EBanking.Api.DB;
using EBanking.Api.Security;

namespace EBanking.Api;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services) =>
        services
        .AddDbContextFactory<EBankingDbContext, EBankingDbContextFactory>()
        .AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>()
        .AddTransient<InitializationHelper>();
}