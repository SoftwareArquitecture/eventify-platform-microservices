using Eventify.Services.Planning.Domain.Model.Commands;
using Eventify.Services.Planning.Domain.Model.ValueObjects;
using Eventify.Services.Planning.Interfaces.REST.Resources;

namespace Eventify.Services.Planning.Interfaces.REST.Transform;

public class CreateServiceItemCommandFromResourceAssembler
{
    public static CreateServiceItemCommand ToCommandFromResource(CreateServiceItemResource resource)
    {
        return new CreateServiceItemCommand(resource.Description, resource.Quantity, resource.UnitPrice,
            resource.TotalPrice, new QuoteId(Guid.Parse(resource.QuoteId)));
    }
}