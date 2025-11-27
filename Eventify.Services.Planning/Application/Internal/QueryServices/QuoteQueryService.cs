using Eventify.Services.Planning.Domain.Model.Aggregates;
using Eventify.Services.Planning.Domain.Model.Queries;
using Eventify.Services.Planning.Domain.Repositories;
using Eventify.Services.Planning.Domain.Services;

namespace Eventify.Services.Planning.Application.Internal.QueryServices;

public class QuoteQueryService(IQuoteRepository quoteRepository) : IQuoteQueryService
{
    public async Task<IEnumerable<Quote>> Handle(GetAllQuotesByOrganizerIdQuery query)
    {
        return await quoteRepository.FindByOrganizerIdAsync(query.OrganizerId);
    }

    public async Task<Quote?> Handle(GetQuoteByIdQuery query)
    {
        return await quoteRepository.FindByIdAsync(query.QuoteId);
    }
}