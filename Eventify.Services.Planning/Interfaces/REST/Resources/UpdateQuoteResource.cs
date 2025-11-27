namespace Eventify.Services.Planning.Interfaces.REST.Resources;

public record UpdateQuoteResource(
    string Title,
    string EventType,
    int GuestQuantity,
    string Location,
    double TotalPrice,
    DateTime EventDate);