using Eventify.Services.Planning.Domain.Model.Aggregates;
using Eventify.Services.Planning.Domain.Model.Queries;

namespace Eventify.Services.Planning.Domain.Services;

public interface IServiceItemQueryService
{
    public Task<ServiceItem?> Handle(GetServiceItemByIdQuery query);

    public Task<IEnumerable<ServiceItem>> Handle(GetAllServiceItemsByQuoteIdQuery query);
}