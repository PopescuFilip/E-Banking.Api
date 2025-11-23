namespace EBanking.Api.Services;

public interface IUserService
{
    bool Exists(string email);
    bool Exists(string email, string password);
    bool Create(string email, string password);
}