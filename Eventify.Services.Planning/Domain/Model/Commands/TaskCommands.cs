namespace Eventify.Services.Planning.Domain.Model.Commands;

public record CreateTaskCommand(
    string Title,
    string Description,
    int ColumnId,
    int Order
);

public record UpdateTaskCommand(
    int Id,
    string Title,
    string Description,
    int ColumnId,
    int Order
);

public record DeleteTaskCommand(int Id);

public record MoveTaskCommand(
    int Id,
    int TargetColumnId,
    int Order
);