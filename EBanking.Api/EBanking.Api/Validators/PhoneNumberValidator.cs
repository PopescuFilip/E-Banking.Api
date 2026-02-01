namespace EBanking.Api.Validators;

public interface IPhoneNumberValidator
{
    bool IsValid(string phoneNumber);
}

public class PhoneNumberValidator : IPhoneNumberValidator
{
    public bool IsValid(string phoneNumber) =>
        phoneNumber.Length == 10 && phoneNumber.All(char.IsDigit);
}