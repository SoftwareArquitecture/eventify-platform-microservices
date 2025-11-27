namespace Eventify.Services.Profiles.Interfaces.REST.Resources;

public record ProfileResource(
    int Id,
    string FullName,
    string Email,
    string StreetAddress,
    string PhoneNumber,
    string WebSite,
    string Biography,
    string Role);