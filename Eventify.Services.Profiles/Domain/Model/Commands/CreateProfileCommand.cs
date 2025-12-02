using Eventify.Services.Profiles.Domain.Model.ValueObjects;

namespace Eventify.Services.Profiles.Domain.Model.Commands;

public record CreateProfileCommand(
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
    TypeProfile Role);