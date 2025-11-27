using Eventify.Services.Operation.Domain.Model.Aggregates;
using Eventify.Services.Operation.Interfaces.REST.Resources;

namespace Eventify.Services.Operation.Interfaces.REST.Transform;

public static class ReviewResourceFromEntityAssembler
{
    public static ReviewResource ToResourceFromEntity(Review entity)
    {
        return new ReviewResource(
            entity.Id,
            entity.Reviewer,
            entity.EventName,
            entity.EventDate,
            entity.Content,
            entity.Rating,
            entity.ReviewDate);
    }
}