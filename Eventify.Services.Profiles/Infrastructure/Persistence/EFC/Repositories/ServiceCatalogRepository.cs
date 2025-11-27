using Eventify.Services.Profiles.Domain.Model.Aggregates;
using Eventify.Services.Profiles.Domain.Repositories;
using Eventify.Shared.Infrastructure.Persistence.EFC.Configuration;
using Eventify.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Eventify.Services.Profiles.Infrastructure.Persistence.EFC.Repositories;

public class ServiceCatalogRepository(AppDbContext context)
    : BaseRepository<ServiceCatalog>(context), IServiceCatalogRepository
{
    public async Task<IEnumerable<ServiceCatalog>> ListByProfileIdAsync(int profileId)
    {
        return await Context.Set<ServiceCatalog>()
            .Where(s => s.ProfileId == profileId)
            .ToListAsync();
    }

    public async Task<ServiceCatalog?> FindByIdAndProfileIdAsync(int serviceCatalogId, int profileId)
    {
        return await Context.Set<ServiceCatalog>()
            .FirstOrDefaultAsync(s => s.Id == serviceCatalogId && s.ProfileId == profileId);
    }
}