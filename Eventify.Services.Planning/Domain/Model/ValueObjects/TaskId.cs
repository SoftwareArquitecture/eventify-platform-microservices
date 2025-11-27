namespace Eventify.Services.Planning.Domain.Model.ValueObjects;

public record TaskId(int Id)
{
    public TaskId() : this(0)
    {
    }

    public static implicit operator int(TaskId taskId)
    {
        return taskId.Id;
    }

    public static implicit operator TaskId(int id)
    {
        return new TaskId(id);
    }
}