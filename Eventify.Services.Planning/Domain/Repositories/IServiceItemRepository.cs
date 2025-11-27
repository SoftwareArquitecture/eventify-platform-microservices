using Eventify.Services.Planning.Domain.Model.Aggregates;
using Eventify.Services.Planning.Domain.Model.ValueObjects;
using Eventify.Shared.Domain.Repositories;

namespace Eventify.Services.Planning.Domain.Repositories;

public interface IServiceItemRepository : IBaseRepository<ServiceItem>
{
    Task<IEnumerable<ServiceItem>> FindByQuoteIdAsync(QuoteId quoteId);

    Task<ServiceItem?> FindByIdAsync(ServiceItemId serviceItemId);
}