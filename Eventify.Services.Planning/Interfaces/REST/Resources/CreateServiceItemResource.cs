namespace Eventify.Services.Planning.Interfaces.REST.Resources;

public record CreateServiceItemResource(
    string Description,
    int Quantity,
    double UnitPrice,
    double TotalPrice,
    string QuoteId);