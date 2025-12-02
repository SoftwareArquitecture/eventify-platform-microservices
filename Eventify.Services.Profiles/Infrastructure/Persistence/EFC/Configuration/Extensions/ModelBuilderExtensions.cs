using Eventify.Services.Profiles.Domain.Model.Aggregates;
using Eventify.Services.Profiles.Domain.Model.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Eventify.Services.Profiles.Infrastructure.Persistence.EFC.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyProfilesConfiguration(this ModelBuilder builder)
    {
        // Apply all entity configurations
        ApplyProfileConfiguration(builder);
        ApplyAlbumsConfiguration(builder);
        ApplyServiceCatalogsConfiguration(builder);
    }

    private static void ApplyProfileConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Profile>().HasKey(p => p.Id);
        builder.Entity<Profile>()
            .Property(p => p.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();
        builder.Entity<Profile>()
            .Property(p => p.UserId)
            .IsRequired();
        builder.Entity<Profile>()
            .HasIndex(p => p.UserId)
            .IsUnique();
        builder.Entity<Profile>().OwnsOne(p => p.Name, n =>
        {
            n.WithOwner().HasForeignKey("Id");
            n.Property(n => n.FirstName).HasColumnName("FirstName");
            n.Property(n => n.LastName).HasColumnName("LastName");
        });
        builder.Entity<Profile>().OwnsOne(p => p.Email, e =>
        {
            e.WithOwner().HasForeignKey("Id");
            e.Property(e => e.Address).HasColumnName("EmailAddress");
        });
        builder.Entity<Profile>().OwnsOne(p => p.Address, a =>
        {
            a.WithOwner().HasForeignKey("Id");
            a.Property(a => a.Street).HasColumnName("AddressStreet");
            a.Property(a => a.Number).HasColumnName("AddressNumber");
            a.Property(a => a.City).HasColumnName("AddressCity");
            a.Property(a => a.PostalCode).HasColumnName("AddressPostalCode");
            a.Property(a => a.Country).HasColumnName("AddressCountry");
        });
        builder.Entity<Profile>().OwnsOne(p => p.PhoneNumber, pn =>
        {
            pn.WithOwner().HasForeignKey("Id");
            pn.Property(pn => pn.Number).HasColumnName("PhoneNumber");
        });
        builder.Entity<Profile>().OwnsOne(p => p.WebSite, w =>
        {
            w.WithOwner().HasForeignKey("Id");
            w.Property(ws => ws.Url).HasColumnName("WebSite");
        });
        builder.Entity<Profile>().OwnsOne(p => p.Biography, b =>
        {
            b.WithOwner().HasForeignKey("Id");
            b.Property(bi => bi.Text).HasColumnName("Biography");
        });
        var RoleConverter = new ValueConverter<TypeProfile, string>(
            t => t.ToString().ToLower(),
            t => Enum.Parse<TypeProfile>(t, true));
        builder.Entity<Profile>()
            .Property(p => p.Role)
            .HasConversion(RoleConverter)
            .IsRequired();
    }

    private static void ApplyAlbumsConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Album>().HasKey(a => a.Id);
        builder.Entity<Album>()
            .Property(a => a.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();
        builder.Entity<Album>()
            .Property(a => a.Name)
            .IsRequired();
        builder.Entity<Album>()
            .Property(a => a.ProfileId)
            .IsRequired();
        builder.Entity<Album>().OwnsMany(a => a.Photos, p =>
        {
            p.WithOwner().HasForeignKey("AlbumId");
            p.Property<int>("Id");
            p.HasKey("Id");
            p.Property(ph => ph.Url).HasColumnName("Url");
        });
    }

    private static void ApplyServiceCatalogsConfiguration(this ModelBuilder builder)
    {
        builder.Entity<ServiceCatalog>().HasKey(s => s.Id);
        builder.Entity<ServiceCatalog>()
            .Property(s => s.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();
        builder.Entity<ServiceCatalog>()
            .Property(s => s.ProfileId)
            .IsRequired();
        builder.Entity<ServiceCatalog>()
            .Property(s => s.Title)
            .IsRequired();
        builder.Entity<ServiceCatalog>()
            .Property(s => s.Description)
            .IsRequired();
        builder.Entity<ServiceCatalog>()
            .Property(s => s.Category)
            .IsRequired();
        builder.Entity<ServiceCatalog>()
            .Property(s => s.PriceFrom)
            .HasColumnName("PriceFrom");
        builder.Entity<ServiceCatalog>()
            .Property(s => s.PriceTo)
            .HasColumnName("PriceTo");
    }
}