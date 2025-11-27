using Eventify.Services.Profiles.Domain.Model.Commands;
using Eventify.Services.Profiles.Domain.Model.Queries;
using Eventify.Services.Profiles.Domain.Model.ValueObjects;
using Eventify.Services.Profiles.Domain.Services;
using Eventify.Services.Profiles.Interfaces.ACL;

namespace Eventify.Services.Profiles.Application.ACL;

public class ProfilesContextFacade(
    IProfileCommandService profileCommandService,
    IProfileQueryService profileQueryService)
    : IProfilesContextFacade
{
    public async Task<int> CreateProfile(
        string firstName,
        string lastName,
        string email,
        string street,
        string number,
        string city,
        string postalCode,
        string country,
        string phoneNumber,
        string webSite,
        string biography,
        string role)
    {
        var createProfileCommand = new CreateProfileCommand(
            firstName,
            lastName,
            email,
            street,
            number,
            city,
            postalCode,
            country,
            phoneNumber,
            webSite,
            biography,
            Enum.TryParse<TypeProfile>(role, out var type) ? type : TypeProfile.Organizer);
        var profile = await profileCommandService.Handle(createProfileCommand);
        return profile?.Id ?? 0;
    }

    public async Task<int> FetchProfileIdByEmail(string email)
    {
        var getProfileByEmailQuery = new GetProfileByEmailQuery(new EmailAddress(email));
        var profile = await profileQueryService.Handle(getProfileByEmailQuery);
        return profile?.Id ?? 0;
    }

    public async Task<bool> ProfileExists(int profileId)
    {
        var query = new GetProfileByIdQuery(profileId);
        var profile = await profileQueryService.Handle(query);
        return profile != null;
    }

    public async Task<bool> ProfileExistsWithRole(int profileId, TypeProfile role)
    {
        var query = new GetProfileByIdQuery(profileId);
        var profile = await profileQueryService.Handle(query);
        return profile is not null && profile.Role == role;
    }
}