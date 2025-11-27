using Eventify.Services.Operation.Domain.Model.Aggregates;
using Eventify.Services.Operation.Domain.Model.Queries;

namespace Eventify.Services.Operation.Domain.Services;

public interface IReviewQueryService
{
    Task<Review?> Handle(GetReviewByIdQuery query);
    Task<IEnumerable<Review>> Handle(GetAllReviewsQuery query);
}