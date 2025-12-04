namespace Eventify.Services.IAM.Interfaces.REST.Resources;

public record SignUpResource(
    string Username,
    string Password,
    string? Email = null,
    string? FirstName = null,
    string? LastName = null,
    string? PhoneNumber = null,
    string? Role = null,
    string? Street = null,
    string? Number = null,
    string? City = null,
    string? PostalCode = null,
    string? Country = null,
    string? WebSite = null,
    string? Biography = null);