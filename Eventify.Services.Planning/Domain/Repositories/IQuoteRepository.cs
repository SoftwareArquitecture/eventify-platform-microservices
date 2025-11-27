using Eventify.Services.Planning.Domain.Model.Aggregates;
using Eventify.Services.Planning.Domain.Model.ValueObjects;
using Eventify.Shared.Domain.Repositories;

namespace Eventify.Services.Planning.Domain.Repositories;

public interface IQuoteRepository : IBaseRepository<Quote>
{
    Task<IEnumerable<Quote>> FindByOrganizerIdAsync(OrganizerId organizerId);
    Task<Quote?> FindByIdAsync(QuoteId quoteId);
}