using Eventify.Services.Planning.Domain.Model.Commands;
using Eventify.Services.Planning.Interfaces.REST.Resources;

namespace Eventify.Services.Planning.Interfaces.REST.Transform;

public static class CreateSocialEventCommandFromResourceAssembler
{
    public static CreateSocialEventCommand ToCommandFromResource(CreateSocialEventResource resource)
    {
        return new CreateSocialEventCommand(
            resource.EventTitle,
            resource.EventDate,
            resource.CustomerName,
            resource.Location,
            resource.Status
        );
    }
}