using Eventify.Services.Operation.Domain.Model.Aggregates;
using Eventify.Services.Operation.Domain.Repositories;
using Eventify.Shared.Infrastructure.Persistence.EFC.Configuration;
using Eventify.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Eventify.Services.Operation.Infrastructure.Persistence.EFC.Repositories;

public class ReviewRepository(AppDbContext context) : BaseRepository<Review>(context), IReviewRepository
{
    public new async Task<Review?> FindByIdAsync(int id)
    {
        return await Context.Set<Review>()
            .FirstOrDefaultAsync(review => review.Id == id);
    }

    public new async Task<IEnumerable<Review>> ListAsync()
    {
        return await Context.Set<Review>()
            .ToListAsync();
    }
}