using Eventify.Services.Profiles.Domain.Model.Commands;
using Eventify.Services.Profiles.Interfaces.REST.Resources;

namespace Eventify.Services.Profiles.Interfaces.REST.Transform;

public class UpdateAlbumCommandFromResourceAssembler
{
    public static UpdateAlbumCommand ToCommandFromResource(int profileId, int albumId, UpdateAlbumResource resource)
    {
        return new UpdateAlbumCommand(profileId, albumId, resource.Name, resource.Photos);
    }
}