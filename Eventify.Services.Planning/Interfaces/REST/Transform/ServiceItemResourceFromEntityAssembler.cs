using Eventify.Services.Planning.Domain.Model.Aggregates;
using Eventify.Services.Planning.Interfaces.REST.Resources;

namespace Eventify.Services.Planning.Interfaces.REST.Transform;

public class ServiceItemResourceFromEntityAssembler
{
    public static ServiceItemResource ToResourceFromEntity(ServiceItem entity)
    {
        return new ServiceItemResource(entity.Id.Identifier.ToString(), entity.Description, entity.Quantity,
            entity.UnitPrice, entity.TotalPrice, entity.QuoteId.Identifier.ToString());
    }
}