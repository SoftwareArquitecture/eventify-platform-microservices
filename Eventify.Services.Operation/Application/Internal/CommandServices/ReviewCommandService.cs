using Eventify.Services.Operation.Domain.Model.Aggregates;
using Eventify.Services.Operation.Domain.Model.Commands;
using Eventify.Services.Operation.Domain.Repositories;
using Eventify.Services.Operation.Domain.Services;
using Eventify.Shared.Domain.Repositories;

namespace Eventify.Services.Operation.Application.Internal.CommandServices;

public class ReviewCommandService(
    IReviewRepository reviewRepository,
    IUnitOfWork unitOfWork
) : IReviewCommandService
{
    public async Task<Review?> Handle(CreateReviewCommand command)
    {
        var review = new Review(command);
        await reviewRepository.AddAsync(review);
        await unitOfWork.CompleteAsync();
        return review;
    }
}