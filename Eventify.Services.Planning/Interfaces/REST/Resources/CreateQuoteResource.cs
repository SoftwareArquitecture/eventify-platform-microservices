namespace Eventify.Services.Planning.Interfaces.REST.Resources;

public record CreateQuoteResource(
    string Title,
    string EventType,
    int GuestQuantity,
    string Location,
    double TotalPrice,
    string Status,
    DateTime EventDate,
    int OrganizerId,
    int HostId);