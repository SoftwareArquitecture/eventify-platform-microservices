using Eventify.Services.Planning.Domain.Model.Aggregates;
using Eventify.Services.Planning.Domain.Model.ValueObjects;
using Eventify.Services.Planning.Domain.Repositories;
using Eventify.Shared.Infrastructure.Persistence.EFC.Configuration;
using Eventify.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Eventify.Services.Planning.Infrastructure.Persistence.EFC.Repositories;

public class ServiceItemRepository(AppDbContext context) : BaseRepository<ServiceItem>(context), IServiceItemRepository
{
    public async Task<ServiceItem?> FindByIdAsync(ServiceItemId serviceItemId)
    {
        return await Context.Set<ServiceItem>().FirstOrDefaultAsync(serviceItem => serviceItem.Id == serviceItemId);
    }

    public async Task<IEnumerable<ServiceItem>> FindByQuoteIdAsync(QuoteId quoteId)
    {
        return await Context.Set<ServiceItem>().Where(serviceItem => serviceItem.QuoteId == quoteId).ToListAsync();
    }
}