using Eventify.Services.Operation.Domain.Model.Aggregates;
using Eventify.Services.Operation.Domain.Model.Queries;
using Eventify.Services.Operation.Domain.Repositories;
using Eventify.Services.Operation.Domain.Services;

namespace Eventify.Services.Operation.Application.Internal.QueryService;

public class ReviewQueryService(IReviewRepository reviewRepository) : IReviewQueryService
{
    public async Task<Review?> Handle(GetReviewByIdQuery query)
    {
        return await reviewRepository.FindByIdAsync(query.Id);
    }

    public async Task<IEnumerable<Review>> Handle(GetAllReviewsQuery query)
    {
        return await reviewRepository.ListAsync();
    }
}