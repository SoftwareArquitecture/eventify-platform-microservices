namespace Eventify.Services.Profiles.Interfaces.REST.Resources;

public record CreateAlbumResource(
    string Name,
    List<string> Photos);