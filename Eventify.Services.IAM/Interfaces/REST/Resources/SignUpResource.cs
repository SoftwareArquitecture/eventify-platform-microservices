namespace Eventify.Services.IAM.Interfaces.REST.Resources;

public record SignUpResource(
    string Username,
    string Password,
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    string Role,
    string? Street = null,
    string? Number = null,
    string? City = null,
    string? PostalCode = null,
    string? Country = null,
    string? WebSite = null,
    string? Biography = null);