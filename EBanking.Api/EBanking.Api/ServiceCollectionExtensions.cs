using EBanking.Api.DB;
using EBanking.Api.Security;
using EBanking.Api.Services;
using EBanking.Api.Validators;
using Microsoft.EntityFrameworkCore;

namespace EBanking.Api;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration) =>
        services
        .AddDbContext<EBankingDbContext>(o => o.UseSqlServer(configuration.GetConnectionString("EBankingDb")))
        .AddServicesLayer()
        .AddValidatorsLayer()
        .AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>()
        .AddTransient<InitializationHelper>();

    private static IServiceCollection AddServicesLayer(this IServiceCollection services) =>
        services
        .AddTransient<IUserService, UserService>();

    private static IServiceCollection AddValidatorsLayer(this IServiceCollection services) =>
        services
        .AddTransient<IEmailValidator, EmailValidator>();
}