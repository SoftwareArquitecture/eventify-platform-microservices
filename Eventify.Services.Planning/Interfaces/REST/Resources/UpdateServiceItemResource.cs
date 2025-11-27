namespace Eventify.Services.Planning.Interfaces.REST.Resources;

public record UpdateServiceItemResource(string Description, int Quantity, double UnitPrice, double TotalPrice);