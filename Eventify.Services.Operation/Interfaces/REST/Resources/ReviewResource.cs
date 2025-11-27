namespace Eventify.Services.Operation.Interfaces.REST.Resources;

public record ReviewResource(
    int Id,
    string Reviewer,
    string EventName,
    DateTime EventDate,
    string Content,
    int Rating,
    DateTime ReviewDate);