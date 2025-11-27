using Eventify.Services.Profiles.Domain.Model.Aggregates;
using Eventify.Services.Profiles.Domain.Model.Queries;

namespace Eventify.Services.Profiles.Domain.Services;

public interface IAlbumQueryService
{
    Task<IEnumerable<Album>> Handle(GetAllAlbumsQuery query);
    Task<Album?> Handle(GetAlbumByIdQuery query);
    Task<IEnumerable<Album>> Handle(GetAlbumsByProfileIdQuery query);
    Task<Album?> Handle(GetAlbumByIdForProfileQuery query);
}