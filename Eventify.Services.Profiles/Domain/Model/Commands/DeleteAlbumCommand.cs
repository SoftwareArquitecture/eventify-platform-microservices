namespace Eventify.Services.Profiles.Domain.Model.Commands;

/**Represents a command to delete an album by its ID*/
public record DeleteAlbumCommand(int ProfileId, int AlbumId);