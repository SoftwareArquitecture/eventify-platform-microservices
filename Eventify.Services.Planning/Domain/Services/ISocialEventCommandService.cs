using Eventify.Services.Planning.Domain.Model.Aggregates;
using Eventify.Services.Planning.Domain.Model.Commands;

namespace Eventify.Services.Planning.Domain.Services;

public interface ISocialEventCommandService
{
    Task<SocialEvent?> Handle(CreateSocialEventCommand command);
    Task<SocialEvent?> Handle(UpdateSocialEventCommand command);
    Task<SocialEvent?> Handle(DeleteSocialEventCommand command);
}