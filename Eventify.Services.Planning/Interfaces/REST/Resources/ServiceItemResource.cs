namespace Eventify.Services.Planning.Interfaces.REST.Resources;

public record ServiceItemResource(
    string Id,
    string Description,
    int Quantity,
    double UnitPrice,
    double TotalPrice,
    string QuoteId);