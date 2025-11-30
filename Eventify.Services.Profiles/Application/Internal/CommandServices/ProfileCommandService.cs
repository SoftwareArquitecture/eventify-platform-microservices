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
}