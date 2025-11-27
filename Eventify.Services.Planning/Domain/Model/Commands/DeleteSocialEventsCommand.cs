namespace Eventify.Services.Planning.Domain.Model.Commands;

public record DeleteSocialEventsCommand(IEnumerable<int> Ids);