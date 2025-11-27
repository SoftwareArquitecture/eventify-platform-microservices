using Eventify.Services.Planning.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eventify.Services.Planning.Infrastructure.Persistence.EFC.Configuration;

public class SocialEventConfiguration : IEntityTypeConfiguration<SocialEvent>
{
    public void Configure(EntityTypeBuilder<SocialEvent> builder)
    {
        // Primary key
        builder.HasKey(se => se.Id);
        builder.Property(se => se.Id).IsRequired().ValueGeneratedOnAdd();

        // Value Object: Title
        builder.OwnsOne(se => se.Title, t =>
        {
            t.WithOwner().HasForeignKey("IdSocialEvent");
            t.Property(p => p.Title)
                .HasColumnName("EventTitle")
                .IsRequired()
                .HasMaxLength(200);
        });

        // Value Object: EventDate
        builder.OwnsOne(se => se.EventDate, ed =>
        {
            ed.WithOwner().HasForeignKey("IdSocialEvent");
            ed.Property(p => p.Date)
                .HasColumnName("EventDate")
                .IsRequired();
        });

        // Value Object: NameCustomer
        builder.OwnsOne(se => se.NameCustomer, c =>
        {
            c.WithOwner().HasForeignKey("IdSocialEvent");
            c.Property(p => p.NameCustomer)
                .HasColumnName("CustomerFirstName")
                .HasMaxLength(100);
        });

        // Value Object: Place
        builder.OwnsOne(se => se.Place, p =>
        {
            p.WithOwner().HasForeignKey("IdSocialEvent");
            p.Property(x => x.Place)
                .HasColumnName("Location")
                .IsRequired()
                .HasMaxLength(300);
        });

        // Enum: Status
        builder.Property(se => se.Status)
            .IsRequired()
            .HasConversion<string>();
    }
}