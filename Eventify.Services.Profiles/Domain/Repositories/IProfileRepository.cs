using Eventify.Services.Profiles.Domain.Model.Aggregates;
using Eventify.Services.Profiles.Domain.Model.ValueObjects;
using Eventify.Shared.Domain.Repositories;

namespace Eventify.Services.Profiles.Domain.Repositories;

public interface IProfileRepository : IBaseRepository<Profile>
{
    Task<Profile?> FindByEmailAsync(EmailAddress email);
    Task<Profile?> FindByUserIdAsync(int userId);
}