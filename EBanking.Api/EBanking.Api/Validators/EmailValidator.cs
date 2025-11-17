using System.Net.Mail;

namespace EBanking.Api.Validators;

public interface IEmailValidator
{
    bool IsValid(string email);
}

public class EmailValidator : IEmailValidator
{
    public bool IsValid(string email) => !string.IsNullOrWhiteSpace(email) && MailAddress.TryCreate(email, out var _);
}