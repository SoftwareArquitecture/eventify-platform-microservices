using Eventify.Services.Planning.Domain.Model.Aggregates;
using Eventify.Services.Planning.Infrastructure.Persistence.EFC.Configuration.Extensions;
using Eventify.Shared.Infrastructure.Persistence.EFC.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Eventify.Services.Planning.Infrastructure.Persistence.EFC.Configuration;

public class PlanningDbContext(DbContextOptions options)
    : AppDbContext(options)
{
    public DbSet<Quote> Quotes { get; set; }
    public DbSet<ServiceItem> ServiceItems { get; set; }
    public DbSet<SocialEvent> SocialEvents { get; set; }
    public DbSet<TaskItem> Tasks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        base.OnConfiguring(builder);
        builder.AddInterceptors(new PublishDomainEventsInterceptor());
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyPlanningConfiguration();
    }
}