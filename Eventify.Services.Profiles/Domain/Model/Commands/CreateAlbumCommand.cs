namespace Eventify.Services.Profiles.Domain.Model.Commands;

public record CreateAlbumCommand(
    int ProfileId,
    string Name,
    IEnumerable<string> Photos);