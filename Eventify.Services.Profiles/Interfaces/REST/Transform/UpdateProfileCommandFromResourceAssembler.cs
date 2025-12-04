using Eventify.Services.Profiles.Domain.Model.Commands;
using Eventify.Services.Profiles.Interfaces.REST.Resources;

namespace Eventify.Services.Profiles.Interfaces.REST.Transform;

public static class UpdateProfileCommandFromResourceAssembler
{
    public static UpdateProfileCommand ToCommandFromResource(int profileId, UpdateProfileResource resource)
    {
        return new UpdateProfileCommand(
            profileId,
            resource.FirstName,
            resource.LastName,
            resource.Email,
            resource.Street,
            resource.Number,
            resource.City,
            resource.PostalCode,
            resource.Country,
            resource.PhoneNumber,
            resource.WebSite,
            resource.Biography,
            resource.Role
        );
    }
}
