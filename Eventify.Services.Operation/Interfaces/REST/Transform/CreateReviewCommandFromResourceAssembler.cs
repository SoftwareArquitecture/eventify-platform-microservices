using Eventify.Services.Operation.Domain.Model.Commands;
using Eventify.Services.Operation.Interfaces.REST.Resources;

namespace Eventify.Services.Operation.Interfaces.REST.Transform;

public static class CreateReviewCommandFromResourceAssembler
{
    public static CreateReviewCommand ToCommandFromResource(CreateReviewResource resource)
    {
        return new CreateReviewCommand(
            resource.Reviewer,
            resource.EventName,
            resource.EventDate,
            resource.Content,
            resource.Rating,
            resource.ReviewDate
        );
    }
}