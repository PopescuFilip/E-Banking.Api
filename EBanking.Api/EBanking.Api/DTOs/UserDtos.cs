namespace EBanking.Api.DTOs;

public record UserDetails(string Name, string PhoneNumber, string Email, string Password);

public record UpdateUserDetailsRequest(string Name, string PhoneNumber, string Password);