using Eventify.Services.Planning.Domain.Model.Aggregates;
using Eventify.Services.Planning.Domain.Model.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Eventify.Services.Planning.Infrastructure.Persistence.EFC.Configuration.Extensions;

public static class ModelBuilderExtension
{
    public static void ApplyPlanningConfiguration(this ModelBuilder builder)
    {
        //Planning ORM Mapping Rules
        var quoteIdConverter = new ValueConverter<QuoteId, Guid>(
            v => v.Identifier,
            v => new QuoteId(v));
        var organizerIdConverter = new ValueConverter<OrganizerId, int>(
            v => v.Identifier,
            v => new OrganizerId(v));
        var hostIdConverter = new ValueConverter<HostId, int>(
            v => v.Identifier,
            v => new HostId(v));

        var eSocialEventTypeConverter = new ValueConverter<ESocialEventType, string>(
            v => v.ToString(),
            v => Enum.Parse<ESocialEventType>(v));

        var eQuoteStatusConverter = new ValueConverter<EQuoteStatus, string>(
            v => v.ToString(),
            v => Enum.Parse<EQuoteStatus>(v));

        builder.Entity<Quote>().HasKey(q => q.Id);
        builder.Entity<Quote>().Property(q => q.Id).IsRequired().HasConversion(quoteIdConverter);
        builder.Entity<Quote>().Property(q => q.Title).IsRequired();
        builder.Entity<Quote>().Property(q => q.EventDate).IsRequired().HasColumnType("datetime");
        builder.Entity<Quote>().Property(q => q.EventType).IsRequired().HasConversion(eSocialEventTypeConverter)
            .HasMaxLength(50);
        builder.Entity<Quote>().Property(q => q.GuestQuantity).IsRequired();
        builder.Entity<Quote>().Property(q => q.Location).IsRequired();
        builder.Entity<Quote>().Property(q => q.HostId).IsRequired().HasConversion(hostIdConverter).HasMaxLength(50);
        builder.Entity<Quote>().Property(q => q.OrganizerId).IsRequired().HasConversion(organizerIdConverter)
            .HasMaxLength(50);
        builder.Entity<Quote>().Property(q => q.TotalPrice).IsRequired();
        builder.Entity<Quote>().Property(q => q.Status).IsRequired().HasConversion(eQuoteStatusConverter)
            .HasMaxLength(50);

        // ORM Mapping Rules for Service Item

        var serviceItemIdConverter = new ValueConverter<ServiceItemId, Guid>(
            v => v.Identifier,
            v => new ServiceItemId(v));
        builder.Entity<ServiceItem>().HasKey(s => s.Id);
        builder.Entity<ServiceItem>().Property(s => s.Id).IsRequired().HasConversion(serviceItemIdConverter);
        builder.Entity<ServiceItem>().Property(s => s.Description).IsRequired();
        builder.Entity<ServiceItem>().Property(s => s.Quantity).IsRequired();
        builder.Entity<ServiceItem>().Property(s => s.UnitPrice).IsRequired();
        builder.Entity<ServiceItem>().Property(s => s.TotalPrice).IsRequired();
        builder.Entity<ServiceItem>().Property(s => s.QuoteId).HasConversion(quoteIdConverter).HasMaxLength(50);

        // ORM Mapping Rules for Social Event
        //SocialEvent ORM Mapping Rules
        var socialEventIdConverter = new ValueConverter<SocialEventId, Guid>(
            v => v.IdSocialEvent,
            v => new SocialEventId(v));

        var socialEventTitleConverter = new ValueConverter<SocialEventTitle, string>(
            v => v.Title,
            v => new SocialEventTitle(v));

        var socialEventDateConverter = new ValueConverter<SocialEventDate, DateTime>(
            v => v.Date,
            v => new SocialEventDate(v));

        var customerNameConverter = new ValueConverter<CustomerName, string>(
            v => v.NameCustomer,
            v => new CustomerName(v));

        var socialEventPlaceConverter = new ValueConverter<SocialEventPlace, string>(
            v => v.Place,
            v => new SocialEventPlace(v));

        var socialEventStatusConverter = new ValueConverter<EStatusType, string>(
            v => v.ToString(),
            v => Enum.Parse<EStatusType>(v));

        builder.Entity<SocialEvent>().HasKey(se => se.Id);
        builder.Entity<SocialEvent>().Property(se => se.Id).IsRequired().HasConversion(socialEventIdConverter);
        builder.Entity<SocialEvent>().Property(se => se.Title).IsRequired().HasConversion(socialEventTitleConverter)
            .HasColumnName("EventTitle").HasMaxLength(200);
        builder.Entity<SocialEvent>().Property(se => se.EventDate).IsRequired().HasConversion(socialEventDateConverter)
            .HasColumnName("EventDate").HasColumnType("datetime");
        builder.Entity<SocialEvent>().Property(se => se.NameCustomer).HasConversion(customerNameConverter)
            .HasColumnName("CustomerFirstName").HasMaxLength(100);
        builder.Entity<SocialEvent>().Property(se => se.Place).IsRequired().HasConversion(socialEventPlaceConverter)
            .HasColumnName("Location").HasMaxLength(300);
        builder.Entity<SocialEvent>().Property(se => se.Status).IsRequired().HasConversion(socialEventStatusConverter)
            .HasMaxLength(50);

        // ORM Mapping Rules for Task
        var taskIdConverter = new ValueConverter<TaskId, int>(
            v => v.Id,
            v => new TaskId(v));

        var taskTitleConverter = new ValueConverter<TaskTitle, string>(
            v => v.Title,
            v => new TaskTitle(v));

        var taskDescriptionConverter = new ValueConverter<TaskDescription, string>(
            v => v.Description,
            v => new TaskDescription(v));

        var taskColumnIdConverter = new ValueConverter<TaskColumnId, int>(
            v => v.ColumnId,
            v => new TaskColumnId(v));

        var taskOrderConverter = new ValueConverter<TaskOrder, int>(
            v => v.Order,
            v => new TaskOrder(v));

        builder.Entity<TaskItem>().HasKey(t => t.Id);
        builder.Entity<TaskItem>().Property(t => t.Id).IsRequired().HasConversion(taskIdConverter)
            .ValueGeneratedOnAdd();
        builder.Entity<TaskItem>().Property(t => t.Title).IsRequired().HasConversion(taskTitleConverter)
            .HasColumnName("Title").HasMaxLength(200);
        builder.Entity<TaskItem>().Property(t => t.Description).HasConversion(taskDescriptionConverter)
            .HasColumnName("Description").HasMaxLength(1000);
        builder.Entity<TaskItem>().Property(t => t.ColumnId).IsRequired().HasConversion(taskColumnIdConverter)
            .HasColumnName("ColumnId");
        builder.Entity<TaskItem>().Property(t => t.Order).IsRequired().HasConversion(taskOrderConverter)
            .HasColumnName("TaskOrder");
        builder.Entity<TaskItem>().Property(t => t.CreatedAt).IsRequired().HasColumnName("CreatedAt")
            .HasColumnType("datetime");
        builder.Entity<TaskItem>().Property(t => t.UpdatedAt).IsRequired().HasColumnName("UpdatedAt")
            .HasColumnType("datetime");
    }
}