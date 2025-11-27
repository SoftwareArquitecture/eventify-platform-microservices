using Eventify.Services.Operation.Domain.Model.Aggregates;
using Eventify.Services.Operation.Infrastructure.Persistence.EFC.Configuration.Extensions;
using Eventify.Shared.Infrastructure.Persistence.EFC.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Eventify.Services.Operation.Infrastructure.Persistence.EFC.Configuration;

public class OperationDbContext(DbContextOptions options)
    : AppDbContext(options)
{
    public DbSet<Review> Reviews { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        base.OnConfiguring(builder);
        builder.AddInterceptors(new PublishDomainEventsInterceptor());
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyOperationConfiguration();
    }
}