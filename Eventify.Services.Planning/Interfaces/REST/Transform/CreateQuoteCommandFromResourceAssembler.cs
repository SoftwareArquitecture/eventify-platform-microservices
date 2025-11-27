using Eventify.Services.Planning.Domain.Model.Commands;
using Eventify.Services.Planning.Domain.Model.ValueObjects;
using Eventify.Services.Planning.Interfaces.REST.Resources;

namespace Eventify.Services.Planning.Interfaces.REST.Transform;

public class CreateQuoteCommandFromResourceAssembler
{
    public static CreateQuoteCommand ToCommandFromResource(CreateQuoteResource resource)
    {
        return new CreateQuoteCommand(resource.Title, Enum.Parse<ESocialEventType>(resource.EventType),
            resource.GuestQuantity, resource.Location, resource.TotalPrice, Enum.Parse<EQuoteStatus>(resource.Status),
            resource.EventDate, new OrganizerId(resource.OrganizerId), new HostId(resource.HostId));
    }
}