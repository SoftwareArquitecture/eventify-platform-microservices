using Eventify.Services.Operation.Domain.Model.Aggregates;
using Eventify.Services.Operation.Domain.Model.Commands;

namespace Eventify.Services.Operation.Domain.Services;

public interface IReviewCommandService
{
    Task<Review?> Handle(CreateReviewCommand command);
}