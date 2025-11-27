namespace Eventify.Services.Planning.Domain.Model.ValueObjects;

/**
 * * Represents the title of a social event.
 * This value object encapsulates the title string to ensure that each social event has a descriptive title.
 */
public record SocialEventTitle(string Title)
{
    public SocialEventTitle() : this(string.Empty)
    {
    }
}