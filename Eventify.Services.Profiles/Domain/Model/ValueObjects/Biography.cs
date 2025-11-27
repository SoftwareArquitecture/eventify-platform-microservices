namespace Eventify.Services.Profiles.Domain.Model.ValueObjects;

public record Biography(string Text)
{
    public Biography() : this(string.Empty)
    {
    }
}