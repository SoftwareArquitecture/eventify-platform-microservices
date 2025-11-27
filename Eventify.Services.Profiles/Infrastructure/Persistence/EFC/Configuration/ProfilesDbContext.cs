using Eventify.Services.Profiles.Domain.Model.Aggregates;
using Eventify.Services.Profiles.Infrastructure.Persistence.EFC.Configuration.Extensions;
using Eventify.Shared.Infrastructure.Persistence.EFC.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Eventify.Services.Profiles.Infrastructure.Persistence.EFC.Configuration;

public class ProfilesDbContext(DbContextOptions options)
    : AppDbContext(options)
{
    public DbSet<Profile> Profiles { get; set; }
    public DbSet<Album> Albums { get; set; }
    public DbSet<ServiceCatalog> ServiceCatalogs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        base.OnConfiguring(builder);
        builder.AddInterceptors(new PublishDomainEventsInterceptor());
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyProfilesConfiguration();
    }
}