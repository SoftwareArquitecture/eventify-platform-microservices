using Eventify.Services.Profiles.Domain.Model.Commands;
using Eventify.Services.Profiles.Interfaces.REST.Resources;

namespace Eventify.Services.Profiles.Interfaces.REST.Transform;

public class CreateServiceCatalogCommandFromResourceAssembler
{
    public static CreateServiceCatalogCommand ToCommandFromResource(int profileId,
        CreateServiceCatalogResource resource)
    {
        return new CreateServiceCatalogCommand(profileId, resource.Title, resource.Description, resource.Category,
            resource.PriceFrom, resource.PriceTo);
    }
}