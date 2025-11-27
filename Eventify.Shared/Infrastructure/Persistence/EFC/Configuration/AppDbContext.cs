using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Eventify.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Eventify.Shared.Infrastructure.Persistence.EFC.Configuration;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        // Add the created and updated interceptor
        builder.AddCreatedUpdatedInterceptor();
        base.OnConfiguring(builder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        //ModelBuilderExtension.ApplyPlanningConfiguration(builder);
        // Use snake case naming convention for the database
        builder.UseSnakeCaseNamingConvention();
    }
}