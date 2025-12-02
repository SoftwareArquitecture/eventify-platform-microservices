using Eventify.Services.Profiles.Domain.Model.Commands;
using Eventify.Services.Profiles.Domain.Model.ValueObjects;
using Eventify.Services.Profiles.Interfaces.REST.Resources;

namespace Eventify.Services.Profiles.Interfaces.REST.Transform;

public class CreateProfileCommandFromResourceAssembler
{
    public static CreateProfileCommand ToCommandFromResource(CreateProfileResource resource)
    {
        return new CreateProfileCommand(
            resource.UserId,
            resource.FirstName, resource.LastName, resource.Email,
            resource.Street, resource.Number, resource.City,
            resource.PostalCode, resource.Country,
            resource.PhoneNumber, resource.WebSite, resource.Biography,
            Enum.TryParse<TypeProfile>(resource.Role, out var role) ? role : TypeProfile.Hoster);
    }
}