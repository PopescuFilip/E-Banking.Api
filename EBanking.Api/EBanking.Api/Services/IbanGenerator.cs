using System.Text;

namespace EBanking.Api.Services;

public interface IIbanGenerator
{
    string Generate(string email);
}

public class IbanGenerator : IIbanGenerator
{
    private const string CountryCode = "RO";
    private const string SWIFT = "EBNK";
    private const int AccountNumberSize = 16;

    private readonly Random _random = new();

    public string Generate(string email)
    {
        var splitIndex = email.IndexOf('@');
        var firstPart = email[..splitIndex];
        var secondPart = email[(splitIndex + 1)..];

        var firstDigit = Math.Abs(HashCode.Combine(firstPart)) % 10;
        var secondDigit = Math.Abs(HashCode.Combine(secondPart)) % 10;
        var accountNumber = GenerateAccountNumber();

        return $"{CountryCode}{firstDigit}{secondDigit}{SWIFT}{accountNumber}";
    }

    private string GenerateAccountNumber()
    {
        return Enumerable.Range(0, AccountNumberSize)
            .Select(_ => _random.Next(10))
            .Aggregate(new StringBuilder(), (builder, current) => builder.Append(current))
            .ToString();
    }
}