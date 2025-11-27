namespace Eventify.Services.Planning.Interfaces.REST.Resources;

public record TaskResource(
    int Id,
    string Title,
    string Description,
    int ColumnId,
    int Order,
    DateTime CreatedAt
);

public record CreateTaskResource(
    string Title,
    string Description,
    int ColumnId,
    int Order
);

public record UpdateTaskResource(
    string Title,
    string Description,
    int ColumnId,
    int Order
);

public record MoveTaskResource(
    int TargetColumnId,
    int Order
);