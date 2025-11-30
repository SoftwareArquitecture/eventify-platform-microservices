using Eventify.Services.Planning.Domain.Model.Aggregates;
using Eventify.Services.Planning.Domain.Model.ValueObjects;
using Eventify.Services.Planning.Domain.Repositories;
using Eventify.Shared.Infrastructure.Persistence.EFC.Configuration;
using Eventify.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Eventify.Services.Planning.Infrastructure.Persistence.EFC.Repositories;

public class QuoteRepository(AppDbContext context) : BaseRepository<Quote>(context), IQuoteRepository
{
    public async Task<IEnumerable<Quote>> FindByOrganizerIdAsync(OrganizerId organizerId)
    {
        return await Context.Set<Quote>()
            .Where(quote => quote.OrganizerId == organizerId)
            .ToListAsync();
    }

    public async Task<Quote?> FindByIdAsync(QuoteId quoteId)
    {
        return await Context.Set<Quote>().FirstOrDefaultAsync(quote => quote.Id == quoteId);
    }
}