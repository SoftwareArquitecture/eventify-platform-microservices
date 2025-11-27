namespace Eventify.Services.Operation.Interfaces.REST.Resources;

public record CreateReviewResource(
    string Reviewer,
    string EventName,
    DateTime EventDate,
    string Content,
    int Rating,
    DateTime ReviewDate);