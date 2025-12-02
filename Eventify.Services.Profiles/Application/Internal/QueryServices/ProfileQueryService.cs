using Eventify.Services.Profiles.Domain.Model.Aggregates;
using Eventify.Services.Profiles.Domain.Model.Queries;
using Eventify.Services.Profiles.Domain.Repositories;
using Eventify.Services.Profiles.Domain.Services;

namespace Eventify.Services.Profiles.Application.Internal.QueryServices;

public class ProfileQueryService(
    IProfileRepository profileRepository
) : IProfileQueryService
{
    public async Task<IEnumerable<Profile>> Handle(GetAllProfilesQuery query)
    {
        return await profileRepository.ListAsync();
    }

    public async Task<Profile?> Handle(GetProfileByIdQuery query)
    {
        return await profileRepository.FindByIdAsync(query.ProfileId);
    }

    public async Task<Profile?> Handle(GetProfileByEmailQuery query)
    {
        return await profileRepository.FindByEmailAsync(query.Email);
    }

    public async Task<Profile?> Handle(GetProfileByUserIdQuery query)
    {
        return await profileRepository.FindByUserIdAsync(query.UserId);
    }
}