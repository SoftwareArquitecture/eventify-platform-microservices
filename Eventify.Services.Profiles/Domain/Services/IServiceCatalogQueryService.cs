using Eventify.Services.Profiles.Domain.Model.Aggregates;
using Eventify.Services.Profiles.Domain.Model.Queries;

namespace Eventify.Services.Profiles.Domain.Services;

public interface IServiceCatalogQueryService
{
    Task<IEnumerable<ServiceCatalog>> Handle(GetAllServiceCatalogsQuery query);
    Task<ServiceCatalog?> Handle(GetServiceCatalogByIdQuery query);
    Task<IEnumerable<ServiceCatalog>> Handle(GetServiceCatalogsByProfileIdQuery query);
    Task<ServiceCatalog?> Handle(GetServiceCatalogByIdForProfileQuery query);
}