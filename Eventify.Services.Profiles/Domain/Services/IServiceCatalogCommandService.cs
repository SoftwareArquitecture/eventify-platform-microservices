using Eventify.Services.Profiles.Domain.Model.Aggregates;
using Eventify.Services.Profiles.Domain.Model.Commands;

namespace Eventify.Services.Profiles.Domain.Services;

public interface IServiceCatalogCommandService
{
    Task<ServiceCatalog?> Handle(CreateServiceCatalogCommand command);
    Task<ServiceCatalog?> Handle(UpdateServiceCatalogCommand command);
    Task<bool> Handle(DeleteServiceCatalogCommand command);
}