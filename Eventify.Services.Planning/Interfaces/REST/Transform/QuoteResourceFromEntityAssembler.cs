using Eventify.Services.Planning.Domain.Model.Aggregates;
using Eventify.Services.Planning.Interfaces.REST.Resources;
using Microsoft.OpenApi.Extensions;

namespace Eventify.Services.Planning.Interfaces.REST.Transform;

public class QuoteResourceFromEntityAssembler
{
    public static QuoteResource ToResourceFromEntity(Quote entity)
    {
        return new QuoteResource(
            entity.Id.Identifier.ToString(),
            entity.Title,
            entity.EventType.GetDisplayName(),
            entity.GuestQuantity,
            entity.Location,
            entity.TotalPrice,
            entity.Status.GetDisplayName(),
            entity.EventDate,
            entity.OrganizerId.Identifier,
            entity.HostId.Identifier
        );
    }
}