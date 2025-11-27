using Eventify.Services.Planning.Domain.Model.Aggregates;
using Eventify.Services.Planning.Domain.Model.Commands;

namespace Eventify.Services.Planning.Domain.Services;

public interface IServiceItemCommandService
{
    public Task<ServiceItem?> Handle(CreateServiceItemCommand command);
    public Task<ServiceItem?> Handle(UpdateServiceItemCommand command);
    public Task Handle(DeleteServiceItemCommand command);
}