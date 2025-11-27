namespace Eventify.Services.Profiles.Domain.Model.ValueObjects;

public record PhotoUrl(string Url)
{
    public PhotoUrl() : this(string.Empty)
    {
    }
}