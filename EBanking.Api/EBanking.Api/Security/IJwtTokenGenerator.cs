namespace EBanking.Api.Security;

public interface IJwtTokenGenerator
{
    string GenerateToken(string username);
}