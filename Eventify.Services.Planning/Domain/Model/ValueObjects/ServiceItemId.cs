namespace Eventify.Services.Planning.Domain.Model.ValueObjects;

public record ServiceItemId(Guid Identifier)
{
    public ServiceItemId() : this(Guid.NewGuid())
    {
    }
}