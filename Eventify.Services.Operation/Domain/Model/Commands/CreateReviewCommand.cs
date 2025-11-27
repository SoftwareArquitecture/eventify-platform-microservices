namespace Eventify.Services.Operation.Domain.Model.Commands;

public record CreateReviewCommand(
    string Reviewer,
    string EventName,
    DateTime EventDate,
    string Content,
    int Rating,
    DateTime ReviewDate
);