using Eventify.Services.Profiles.Domain.Model.Commands;
using Eventify.Services.Profiles.Interfaces.REST.Resources;

namespace Eventify.Services.Profiles.Interfaces.REST.Transform;

public class CreateAlbumCommandFromResourceAssembler
{
    public static CreateAlbumCommand ToCommandFromResource(int profileId, CreateAlbumResource resource)
    {
        return new CreateAlbumCommand(profileId, resource.Name, resource.Photos);
    }
}