using Eventify.Services.Planning.Domain.Model.ValueObjects;

namespace Eventify.Services.Planning.Domain.Model.Commands;

public record DeleteServiceItemCommand(ServiceItemId ServiceItemId);