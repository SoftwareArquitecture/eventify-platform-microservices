using Eventify.Services.Profiles.Domain.Model.Aggregates;
using Eventify.Services.Profiles.Domain.Model.Commands;
using Eventify.Services.Profiles.Domain.Repositories;
using Eventify.Services.Profiles.Domain.Services;
using Eventify.Shared.Domain.Repositories;

namespace Eventify.Services.Profiles.Application.Internal.CommandServices;

public class ProfileCommandService(
    IProfileRepository profileRepository,
    IUnitOfWork unitOfWork) : IProfileCommandService
{
    public async Task<Profile?> Handle(CreateProfileCommand command)
    {
        var profile = new Profile(command);
        try
        {
            await profileRepository.AddAsync(profile);
            await unitOfWork.CompleteAsync();
            return profile;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<Profile?> Handle(UpdateProfileCommand command)
    {
        var profile = await profileRepository.FindByIdAsync(command.ProfileId);
        if (profile == null) return null;

        profile.UpdateProfile(command);

        try
        {
            profileRepository.Update(profile);
            await unitOfWork.CompleteAsync();
            return profile;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task Handle(DeleteProfileCommand command)
    {
        var profile = await profileRepository.FindByIdAsync(command.ProfileId);
        if (profile == null)
        {
            throw new Exception($"Profile with id '{command.ProfileId}' not found");
        }

        try
        {
            profileRepository.Remove(profile);
            await unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            throw new Exception($"An error occurred while deleting profile: {e.Message}");
        }
    }
}