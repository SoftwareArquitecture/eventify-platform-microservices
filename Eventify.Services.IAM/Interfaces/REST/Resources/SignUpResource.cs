namespace Eventify.Services.IAM.Interfaces.REST.Resources;

public record SignUpResource(
    string Username,
    string Password,
    string Email,
    string FirstName,
    string LastName,
    string PhoneNumber,
    string Street,
    string City,
    string Country,
    string? Role = null);