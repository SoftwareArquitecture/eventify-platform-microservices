using Eventify.Services.Profiles.Domain.Model.Aggregates;
using Eventify.Services.Profiles.Domain.Model.Commands;

namespace Eventify.Services.Profiles.Domain.Services;

public interface IAlbumCommandService
{
    Task<Album?> Handle(CreateAlbumCommand command);
    Task<Album?> Handle(UpdateAlbumCommand command);
    Task<bool> Handle(DeleteAlbumCommand command);
}