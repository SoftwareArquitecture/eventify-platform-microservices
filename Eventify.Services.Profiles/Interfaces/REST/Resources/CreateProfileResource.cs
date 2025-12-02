namespace Eventify.Services.Profiles.Interfaces.REST.Resources;

public record CreateProfileResource(
    int UserId,
    string FirstName,
    string LastName,
    string Email,
    string Street,
    string Number,
    string City,
    string PostalCode,
    string Country,
    string PhoneNumber,
    string WebSite,
    string Biography,
    string Role);