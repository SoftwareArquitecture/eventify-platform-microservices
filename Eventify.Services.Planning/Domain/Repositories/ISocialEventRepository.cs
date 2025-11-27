using Eventify.Services.Planning.Domain.Model.Aggregates;
using Eventify.Services.Planning.Domain.Model.ValueObjects;
using Eventify.Shared.Domain.Repositories;

namespace Eventify.Services.Planning.Domain.Repositories;

public interface ISocialEventRepository : IBaseRepository<SocialEvent>
{
    Task<SocialEvent?> FindByEventTitleAsync(string eventTitle);

    Task<IEnumerable<SocialEvent>> FindByCustomerNameAsync(string customerName);

    Task<IEnumerable<SocialEvent>> FindByLocationAsync(string location);

    Task<IEnumerable<SocialEvent>> FindByDateRangeAsync(DateTime startDate, DateTime endDate);

    Task<IEnumerable<SocialEvent>> FindByStatusAsync(EStatusType status);
}