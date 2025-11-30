using Eventify.Services.Planning.Domain.Model.Aggregates;
using Eventify.Services.Planning.Domain.Model.Queries;
using Eventify.Services.Planning.Domain.Repositories;
using Eventify.Services.Planning.Domain.Services;
using Eventify.Shared.Domain.Repositories;

namespace Eventify.Services.Planning.Application.Internal.QueryServices;

public class ServiceItemQueryService(IServiceItemRepository serviceItemRepository)
    : IServiceItemQueryService
{
    public async Task<ServiceItem?> Handle(GetServiceItemByIdQuery query)
    {
        return await serviceItemRepository.FindByIdAsync(query.ServiceItemId);
    }

    public async Task<IEnumerable<ServiceItem>> Handle(GetAllServiceItemsByQuoteIdQuery query)
    {
        return await serviceItemRepository.FindByQuoteIdAsync(query.QuoteId);
    }
}