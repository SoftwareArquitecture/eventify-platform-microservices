using Eventify.Services.Planning.Domain.Model.Aggregates;
using Eventify.Services.Planning.Domain.Model.Queries;

namespace Eventify.Services.Planning.Domain.Services;

public interface IQuoteQueryService
{
    public Task<IEnumerable<Quote>> Handle(GetAllQuotesByOrganizerIdQuery query);
    public Task<Quote?> Handle(GetQuoteByIdQuery query);
}