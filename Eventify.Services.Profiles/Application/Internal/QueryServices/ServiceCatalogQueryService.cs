using Eventify.Services.Profiles.Domain.Model.Aggregates;
using Eventify.Services.Profiles.Domain.Model.Queries;
using Eventify.Services.Profiles.Domain.Repositories;
using Eventify.Services.Profiles.Domain.Services;

namespace Eventify.Services.Profiles.Application.Internal.QueryServices;

public class ServiceCatalogQueryService(
    IServiceCatalogRepository serviceCatalogRepository
) : IServiceCatalogQueryService
{
    public async Task<IEnumerable<ServiceCatalog>> Handle(GetAllServiceCatalogsQuery query)
    {
        return await serviceCatalogRepository.ListAsync();
    }

    public async Task<ServiceCatalog?> Handle(GetServiceCatalogByIdQuery query)
    {
        return await serviceCatalogRepository.FindByIdAsync(query.ServiceCatalogId);
    }

    public async Task<IEnumerable<ServiceCatalog>> Handle(GetServiceCatalogsByProfileIdQuery query)
    {
        return await serviceCatalogRepository.ListByProfileIdAsync(query.ProfileId);
    }

    public async Task<ServiceCatalog?> Handle(GetServiceCatalogByIdForProfileQuery query)
    {
        return await serviceCatalogRepository.FindByIdAndProfileIdAsync(query.ServiceCatalogId, query.ProfileId);
    }
}