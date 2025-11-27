using Eventify.Services.Planning.Domain.Model.Commands;
using Eventify.Services.Planning.Domain.Model.ValueObjects;
using Eventify.Services.Planning.Interfaces.REST.Resources;

namespace Eventify.Services.Planning.Interfaces.REST.Transform;

public class UpdateQuoteCommandFromResourceAssembler
{
    public static UpdateQuoteCommand ToCommandFromResource(string quoteId, UpdateQuoteResource resource)
    {
        return new UpdateQuoteCommand(new QuoteId(Guid.Parse(quoteId)), resource.Title,
            Enum.Parse<ESocialEventType>(resource.EventType), resource.GuestQuantity, resource.Location,
            resource.TotalPrice, resource.EventDate);
    }
}