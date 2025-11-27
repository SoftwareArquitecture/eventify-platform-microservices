using Eventify.Services.Planning.Domain.Model.Commands;
using Eventify.Services.Planning.Domain.Model.ValueObjects;
using Eventify.Services.Planning.Interfaces.REST.Resources;

namespace Eventify.Services.Planning.Interfaces.REST.Transform;

public class UpdateServiceItemCommandFromResourceAssembler
{
    public static UpdateServiceItemCommand ToCommandFromResource(string serviceItemId,
        UpdateServiceItemResource resource)
    {
        return new UpdateServiceItemCommand(new ServiceItemId(Guid.Parse(serviceItemId)), resource.Description,
            resource.Quantity, resource.UnitPrice, resource.TotalPrice);
    }
}