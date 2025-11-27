using Eventify.Services.Operation.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace Eventify.Services.Operation.Infrastructure.Persistence.EFC.Configuration.Extensions;

public static class ModelBuilderExtension
{
    public static void ApplyOperationConfiguration(this ModelBuilder builder)
    {
        // Operation COntext ORM Mapping Rules
        builder.Entity<Review>().HasKey(r => r.Id);
        builder.Entity<Review>().Property(r => r.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Review>().Property(r => r.Content).IsRequired();
        builder.Entity<Review>().Property(r => r.Rating).IsRequired();
        builder.Entity<Review>().Property(r => r.Reviewer).IsRequired();
        builder.Entity<Review>().Property(r => r.EventName).IsRequired();
        builder.Entity<Review>().Property(r => r.EventDate).IsRequired().HasColumnType("datetime");
        /*builder.Entity<Review>().Property(r => r.ProfileId).HasConversion(p => p.ProfileIdentifier, v => new ProfileId(v));
        builder.Entity<Review>().Property(r => r.SocialEventId)
            .HasConversion(s => s.SocialEventIdentifier, s => new SocialEventId(s));
        */
    }
}