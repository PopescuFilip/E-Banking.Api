using EBanking.Api.DB;
using EBanking.Api.Security;
using EBanking.Api.Services;

namespace EBanking.Api;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services) =>
        services
        .AddDbContextFactory<EBankingDbContext, EBankingDbContextFactory>()
        .AddServicesLayer()
        .AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>()
        .AddTransient<InitializationHelper>();

    private static IServiceCollection AddServicesLayer(this IServiceCollection services) =>
        services.
        AddTransient<IUserService, UserService>();
}