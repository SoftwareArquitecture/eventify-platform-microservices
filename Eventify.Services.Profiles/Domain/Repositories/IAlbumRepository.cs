using Eventify.Services.Profiles.Domain.Model.Aggregates;
using Eventify.Shared.Domain.Repositories;

namespace Eventify.Services.Profiles.Domain.Repositories;

public interface IAlbumRepository : IBaseRepository<Album>
{
    Task<IEnumerable<Album>> ListByProfileIdAsync(int profileId);
    Task<Album?> FindByIdAndProfileIdAsync(int albumId, int profileId);
}