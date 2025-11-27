namespace Eventify.Services.Profiles.Domain.Model.Commands;

public record CreateServiceCatalogCommand(
    int ProfileId,
    string Title,
    string Description,
    string Category,
    decimal PriceFrom,
    decimal PriceTo);