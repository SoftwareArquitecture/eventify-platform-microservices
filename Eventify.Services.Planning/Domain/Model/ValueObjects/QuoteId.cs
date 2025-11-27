namespace Eventify.Services.Planning.Domain.Model.ValueObjects;

public record QuoteId(Guid Identifier)
{
    public QuoteId() : this(Guid.NewGuid())
    {
    }
}