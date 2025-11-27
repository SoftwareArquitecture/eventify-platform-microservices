using Eventify.Services.Profiles.Domain.Model.Aggregates;
using Eventify.Services.Profiles.Interfaces.REST.Resources;

namespace Eventify.Services.Profiles.Interfaces.REST.Transform;

public class AlbumResourceFromEntityAssembler
{
    public static AlbumResource ToResourceFromEntity(Album entity)
    {
        var photos = entity.Photos.Select(p => p.Url);
        return new AlbumResource(entity.Id, entity.ProfileId, entity.Name, photos);
    }
}