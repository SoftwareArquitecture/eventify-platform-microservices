using Eventify.Services.Profiles.Domain.Model.Aggregates;
using Eventify.Services.Profiles.Interfaces.REST.Resources;

namespace Eventify.Services.Profiles.Interfaces.REST.Transform;

public class ServiceCatalogResourceFromEntityAssembler
{
    public static ServiceCatalogResource ToResourceFromEntity(ServiceCatalog entity)
    {
        return new ServiceCatalogResource(entity.Id, entity.ProfileId, entity.Title, entity.Description,
            entity.Category, entity.PriceFrom, entity.PriceTo);
    }
}