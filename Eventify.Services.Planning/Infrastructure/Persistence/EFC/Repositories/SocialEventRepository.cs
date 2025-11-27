using Eventify.Services.Planning.Domain.Model.Aggregates;
using Eventify.Services.Planning.Domain.Model.ValueObjects;
using Eventify.Services.Planning.Domain.Repositories;
using Eventify.Shared.Infrastructure.Persistence.EFC.Configuration;
using Eventify.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Eventify.Services.Planning.Infrastructure.Persistence.EFC.Repositories;

public class SocialEventRepository(AppDbContext context) : BaseRepository<SocialEvent>(context), ISocialEventRepository
{
    public async Task<SocialEvent?> FindByEventTitleAsync(string eventTitle)
    {
        return await Context.Set<SocialEvent>()
            .FirstOrDefaultAsync(se => se.EventTitle == eventTitle);
    }

    public async Task<IEnumerable<SocialEvent>> FindByCustomerNameAsync(string customerName)
    {
        return await Context.Set<SocialEvent>()
            .Where(se => se.NameCustomer.NameCustomer.Contains(customerName))
            .ToListAsync();
    }

    public async Task<IEnumerable<SocialEvent>> FindByLocationAsync(string location)
    {
        return await Context.Set<SocialEvent>()
            .Where(se => se.Location.Contains(location))
            .ToListAsync();
    }

    public async Task<IEnumerable<SocialEvent>> FindByDateRangeAsync(DateTime startDate, DateTime endDate)
    {
        return await Context.Set<SocialEvent>()
            .Where(se => se.EventDate.Date >= startDate && se.EventDate.Date <= endDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<SocialEvent>> FindByStatusAsync(EStatusType status)
    {
        return await Context.Set<SocialEvent>()
            .Where(se => se.Status == status)
            .ToListAsync();
    }
}