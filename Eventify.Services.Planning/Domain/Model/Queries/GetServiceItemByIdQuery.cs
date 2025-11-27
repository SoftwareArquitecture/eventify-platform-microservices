using Eventify.Services.Planning.Domain.Model.ValueObjects;

namespace Eventify.Services.Planning.Domain.Model.Queries;

public record GetServiceItemByIdQuery(ServiceItemId ServiceItemId);