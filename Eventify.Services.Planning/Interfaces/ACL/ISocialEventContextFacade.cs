using Eventify.Services.Planning.Domain.Model.Aggregates;
using Eventify.Services.Planning.Domain.Model.ValueObjects;

namespace Eventify.Services.Planning.Interfaces.ACL;

public interface ISocialEventContextFacade
{
    Task<string> CreateSocialEventAsync(string eventTitle, DateTime eventDate, string customerName, string location,
        EStatusType status);

    Task<SocialEvent?> GetSocialEventByIdAsync(int socialEventId);
    Task<IEnumerable<SocialEvent>> GetAllSocialEventsAsync();

    Task<bool> UpdateSocialEventAsync(int socialEventId, string eventTitle, DateTime eventDate, string customerName,
        string location, EStatusType status);

    Task<bool> DeleteSocialEventAsync(int socialEventId);
    Task<IEnumerable<SocialEvent>> GetSocialEventsByCustomerNameAsync(string customerName);
}