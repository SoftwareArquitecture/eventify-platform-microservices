namespace Eventify.Services.Planning.Domain.Model.ValueObjects;

/**
 * * Represents the place where a social event is held.
 * This value object encapsulates the place string to ensure that each social event has a specific place.
 */
public record SocialEventPlace(string Place)
{
    public SocialEventPlace() : this(string.Empty)
    {
    }
}