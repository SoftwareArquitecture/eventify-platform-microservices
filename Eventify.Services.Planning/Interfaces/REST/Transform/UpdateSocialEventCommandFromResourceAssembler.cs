using Eventify.Services.Planning.Domain.Model.Commands;
using Eventify.Services.Planning.Interfaces.REST.Resources;

namespace Eventify.Services.Planning.Interfaces.REST.Transform;

public static class UpdateSocialEventCommandFromResourceAssembler
{
    public static UpdateSocialEventCommand ToCommandFromResource(int socialEventId, UpdateSocialEventResource resource)
    {
        return new UpdateSocialEventCommand(
            socialEventId,
            resource.EventTitle,
            resource.EventDate,
            resource.CustomerName,
            resource.Location,
            resource.Status
        );
    }
}