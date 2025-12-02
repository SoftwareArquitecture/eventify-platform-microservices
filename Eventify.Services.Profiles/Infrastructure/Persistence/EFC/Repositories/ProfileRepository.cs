using Eventify.Services.Profiles.Domain.Model.Aggregates;
using Eventify.Services.Profiles.Domain.Model.ValueObjects;
using Eventify.Services.Profiles.Domain.Repositories;
using Eventify.Shared.Infrastructure.Persistence.EFC.Configuration;
using Eventify.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Eventify.Services.Profiles.Infrastructure.Persistence.EFC.Repositories;

public class ProfileRepository(AppDbContext context) : BaseRepository<Profile>(context), IProfileRepository
{
    public async Task<Profile?> FindByEmailAsync(EmailAddress email)
    {
        return await Context.Set<Profile>().FirstOrDefaultAsync(p => p.Email.Address == email.Address);
    }

    public async Task<Profile?> FindByUserIdAsync(int userId)
    {
        return await Context.Set<Profile>().FirstOrDefaultAsync(p => p.UserId == userId);
    }
}