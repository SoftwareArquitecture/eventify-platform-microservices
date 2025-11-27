using Eventify.Services.Planning.Domain.Model.ValueObjects;

namespace Eventify.Services.Planning.Interfaces.REST.Resources;

public record CreateSocialEventResource(
    string EventTitle,
    DateTime EventDate,
    string CustomerName,
    string Location,
    EStatusType Status
);