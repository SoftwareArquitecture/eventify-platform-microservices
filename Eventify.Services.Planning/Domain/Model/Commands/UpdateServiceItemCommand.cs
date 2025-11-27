using Eventify.Services.Planning.Domain.Model.ValueObjects;

namespace Eventify.Services.Planning.Domain.Model.Commands;

public record UpdateServiceItemCommand(
    ServiceItemId ServiceItemId,
    string Description,
    int Quantity,
    double UnitPrice,
    double TotalPrice);