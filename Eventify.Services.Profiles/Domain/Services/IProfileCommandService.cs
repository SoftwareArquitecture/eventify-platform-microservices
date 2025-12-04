using Eventify.Services.Profiles.Domain.Model.Aggregates;
using Eventify.Services.Profiles.Domain.Model.Commands;

namespace Eventify.Services.Profiles.Domain.Services;

public interface IProfileCommandService
{
    Task<Profile?> Handle(CreateProfileCommand command);
    Task<Profile?> Handle(UpdateProfileCommand command);
    Task Handle(DeleteProfileCommand command);
}