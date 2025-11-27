using Eventify.Services.Profiles.Domain.Model.Aggregates;
using Eventify.Services.Profiles.Domain.Model.Commands;
using Eventify.Services.Profiles.Domain.Model.ValueObjects;
using Eventify.Services.Profiles.Domain.Repositories;
using Eventify.Services.Profiles.Domain.Services;
using Eventify.Services.Profiles.Interfaces.ACL;
using Eventify.Shared.Domain.Repositories;

namespace Eventify.Services.Profiles.Application.Internal.CommandServices;

public class AlbumCommandService(
    IAlbumRepository albumRepository,
    IUnitOfWork unitOfWork,
    IProfilesContextFacade profilesContextFacade) : IAlbumCommandService
{
    public async Task<Album?> Handle(CreateAlbumCommand command)
    {
        var profileIsOrganizer =
            await profilesContextFacade.ProfileExistsWithRole(command.ProfileId, TypeProfile.Organizer);
        if (!profileIsOrganizer) return null;
        if (command.Photos.Count() > 10) return null;
        var album = new Album(command);
        try
        {
            await albumRepository.AddAsync(album);
            await unitOfWork.CompleteAsync();
            return album;
        }
        catch
        {
            return null;
        }
    }

    public async Task<Album?> Handle(UpdateAlbumCommand command)
    {
        var album = await albumRepository.FindByIdAsync(command.AlbumId);
        if (album is null) return null;
        var profileIsOrganizer =
            await profilesContextFacade.ProfileExistsWithRole(album.ProfileId, TypeProfile.Organizer);
        if (!profileIsOrganizer) return null;
        if (command.Photos.Count() > 10) return null;
        album.Update(command.Name, command.Photos);
        try
        {
            albumRepository.Update(album);
            await unitOfWork.CompleteAsync();
            return album;
        }
        catch
        {
            return null;
        }
    }

    public async Task<bool> Handle(DeleteAlbumCommand command)
    {
        var album = await albumRepository.FindByIdAsync(command.AlbumId);
        if (album is null) return false;
        var profileIsOrganizer =
            await profilesContextFacade.ProfileExistsWithRole(album.ProfileId, TypeProfile.Organizer);
        if (!profileIsOrganizer) return false;
        try
        {
            albumRepository.Remove(album);
            await unitOfWork.CompleteAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }
}