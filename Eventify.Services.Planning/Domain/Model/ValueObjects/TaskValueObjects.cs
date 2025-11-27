namespace Eventify.Services.Planning.Domain.Model.ValueObjects;

public record TaskTitle(string Title)
{
    public TaskTitle() : this(string.Empty)
    {
    }
}

public record TaskDescription(string Description)
{
    public TaskDescription() : this(string.Empty)
    {
    }
}

public record TaskOrder(int Order)
{
    public TaskOrder() : this(0)
    {
    }
}

public record TaskColumnId(int ColumnId)
{
    public TaskColumnId() : this(0)
    {
    }
}