namespace Eventify.Services.Profiles.Interfaces.REST.Resources;

public record UpdateServiceCatalogResource(
    string Title,
    string Description,
    string Category,
    decimal PriceFrom,
    decimal PriceTo);