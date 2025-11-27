using Eventify.Services.Profiles.Domain.Model.Commands;

namespace Eventify.Services.Profiles.Domain.Model.Aggregates;

public class ServiceCatalog
{
    public ServiceCatalog()
    {
        Title = string.Empty;
        Description = string.Empty;
        Category = string.Empty;
    }

    public ServiceCatalog(int profileId, string title, string description, string category, decimal priceFrom,
        decimal priceTo)
    {
        ProfileId = profileId;
        Title = title;
        Description = description;
        Category = category;
        PriceFrom = priceFrom;
        PriceTo = priceTo;
    }

    public ServiceCatalog(CreateServiceCatalogCommand command)
        : this(command.ProfileId, command.Title, command.Description, command.Category, command.PriceFrom,
            command.PriceTo)
    {
    }

    public int Id { get; }
    public int ProfileId { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public string Category { get; private set; }
    public decimal PriceFrom { get; private set; }
    public decimal PriceTo { get; private set; }

    public void Update(string title, string description, string category, decimal priceFrom, decimal priceTo)
    {
        Title = title;
        Description = description;
        Category = category;
        PriceFrom = priceFrom;
        PriceTo = priceTo;
    }
}