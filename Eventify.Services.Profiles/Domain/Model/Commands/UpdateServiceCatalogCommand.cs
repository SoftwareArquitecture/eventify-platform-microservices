namespace Eventify.Services.Profiles.Domain.Model.Commands;

public record UpdateServiceCatalogCommand(
    int ProfileId,
    int ServiceCatalogId,
    string Title,
    string Description,
    string Category,
    decimal PriceFrom,
    decimal PriceTo);