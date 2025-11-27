using Eventify.Services.Planning.Domain.Model.Commands;
using Eventify.Services.Planning.Domain.Model.ValueObjects;

namespace Eventify.Services.Planning.Domain.Model.Aggregates;

public class TaskItem
{
    protected TaskItem()
    {
    }

    public TaskItem(CreateTaskCommand command)
        : this(command.Title, command.Description, command.ColumnId, command.Order)
    {
    }

    public TaskItem(string title, string description, int columnId, int order)
        : this(new TaskTitle(title), new TaskDescription(description), new TaskColumnId(columnId), new TaskOrder(order))
    {
    }

    public TaskItem(TaskTitle title, TaskDescription description, TaskColumnId columnId, TaskOrder order)
    {
        Id = new TaskId();
        Title = title;
        Description = description;
        ColumnId = columnId;
        Order = order;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public TaskId Id { get; private set; }
    public TaskTitle Title { get; private set; }
    public TaskDescription Description { get; private set; }
    public TaskColumnId ColumnId { get; private set; }
    public TaskOrder Order { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    public TaskItem UpdateInformation(TaskTitle title, TaskDescription description, TaskColumnId columnId,
        TaskOrder order)
    {
        Title = title;
        Description = description;
        ColumnId = columnId;
        Order = order;
        UpdatedAt = DateTime.UtcNow;
        return this;
    }

    public TaskItem MoveToColumn(TaskColumnId newColumnId, TaskOrder newOrder)
    {
        ColumnId = newColumnId;
        Order = newOrder;
        UpdatedAt = DateTime.UtcNow;
        return this;
    }
}