using Eventify.Services.Profiles.Domain.Model.Aggregates;
using Eventify.Services.Profiles.Interfaces.REST.Resources;

namespace Eventify.Services.Profiles.Interfaces.REST.Transform;

public class ProfileResourceFromEntityAssembler
{
    public static ProfileResource ToResourceFromEntity(Profile entity)
    {
        return new ProfileResource(
            entity.Id,
            entity.FullName,
            entity.EmailAddress,
            entity.StreetAddress,
            entity.PhoneNumberValue,
            entity.WebSiteUrl,
            entity.BiographyText,
            entity.Role.ToString());
    }
}