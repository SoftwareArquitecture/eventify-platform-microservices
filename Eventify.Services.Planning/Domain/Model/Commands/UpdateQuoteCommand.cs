using Eventify.Services.Planning.Domain.Model.ValueObjects;

namespace Eventify.Services.Planning.Domain.Model.Commands;

public record UpdateQuoteCommand(
    QuoteId QuoteId,
    string Title,
    ESocialEventType EventType,
    int GuestQuantity,
    string Location,
    double TotalPrice,
    DateTime EventDate);