using Eventify.Services.Profiles.Domain.Model.Commands;
using Eventify.Services.Profiles.Interfaces.REST.Resources;

namespace Eventify.Services.Profiles.Interfaces.REST.Transform;

public class UpdateServiceCatalogCommandFromResourceAssembler
{
    public static UpdateServiceCatalogCommand ToCommandFromResource(int profileId, int serviceCatalogId,
        UpdateServiceCatalogResource resource)
    {
        return new UpdateServiceCatalogCommand(profileId, serviceCatalogId, resource.Title, resource.Description,
            resource.Category, resource.PriceFrom, resource.PriceTo);
    }
}