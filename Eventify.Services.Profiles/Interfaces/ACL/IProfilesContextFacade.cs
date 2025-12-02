using Eventify.Services.Profiles.Domain.Model.ValueObjects;

namespace Eventify.Services.Profiles.Interfaces.ACL;

public interface IProfilesContextFacade
{
    Task<int> CreateProfile(
        int userId,
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
        string role);

    Task<int> FetchProfileIdByEmail(string email);
    Task<bool> ProfileExists(int profileId);
    Task<bool> ProfileExistsWithRole(int profileId, TypeProfile role);
}