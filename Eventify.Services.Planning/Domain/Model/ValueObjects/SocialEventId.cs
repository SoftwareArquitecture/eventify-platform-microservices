namespace Eventify.Services.Planning.Domain.Model.ValueObjects;

/**
 * Represents a unique identifier for a social event.
 * This value object encapsulates a GUID to ensure that each social event has a unique ID.
 */
public record SocialEventId(Guid IdSocialEvent)
{
    public SocialEventId() : this(Guid.NewGuid())
    {
    }
}