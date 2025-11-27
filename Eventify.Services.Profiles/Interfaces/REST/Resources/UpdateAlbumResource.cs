namespace Eventify.Services.Profiles.Interfaces.REST.Resources;

/**
 * Represents the resource for updating an album.
 */
public record UpdateAlbumResource(
    string Name,
    List<string> Photos);