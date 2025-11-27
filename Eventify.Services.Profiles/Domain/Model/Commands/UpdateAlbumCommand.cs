namespace Eventify.Services.Profiles.Domain.Model.Commands;

/**
 * Represents a command to update an album.
 */
public record UpdateAlbumCommand(
    int ProfileId,
    int AlbumId,
    string Name,
    IEnumerable<string> Photos);