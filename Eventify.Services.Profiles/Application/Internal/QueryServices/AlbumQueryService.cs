using Eventify.Services.Profiles.Domain.Model.Aggregates;
using Eventify.Services.Profiles.Domain.Model.Queries;
using Eventify.Services.Profiles.Domain.Repositories;
using Eventify.Services.Profiles.Domain.Services;

namespace Eventify.Services.Profiles.Application.Internal.QueryServices;

public class AlbumQueryService(
    IAlbumRepository albumRepository
) : IAlbumQueryService
{
    public async Task<IEnumerable<Album>> Handle(GetAllAlbumsQuery query)
    {
        return await albumRepository.ListAsync();
    }

    public async Task<Album?> Handle(GetAlbumByIdQuery query)
    {
        return await albumRepository.FindByIdAsync(query.AlbumId);
    }

    public async Task<IEnumerable<Album>> Handle(GetAlbumsByProfileIdQuery query)
    {
        return await albumRepository.ListByProfileIdAsync(query.ProfileId);
    }

    public async Task<Album?> Handle(GetAlbumByIdForProfileQuery query)
    {
        return await albumRepository.FindByIdAndProfileIdAsync(query.AlbumId, query.ProfileId);
    }
}