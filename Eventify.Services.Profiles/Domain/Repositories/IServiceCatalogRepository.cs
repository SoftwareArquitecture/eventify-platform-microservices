using Eventify.Services.Profiles.Domain.Model.Aggregates;
using Eventify.Shared.Domain.Repositories;

namespace Eventify.Services.Profiles.Domain.Repositories;

public interface IServiceCatalogRepository : IBaseRepository<ServiceCatalog>
{
    Task<IEnumerable<ServiceCatalog>> ListByProfileIdAsync(int profileId);
    Task<ServiceCatalog?> FindByIdAndProfileIdAsync(int serviceCatalogId, int profileId);
}