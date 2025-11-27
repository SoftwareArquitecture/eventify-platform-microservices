using Eventify.Services.Planning.Domain.Model.Aggregates;
using Eventify.Services.Planning.Domain.Model.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Eventify.Services.Planning.Infrastructure.Persistence.EFC.Configuration;

public class TaskConfiguration : IEntityTypeConfiguration<TaskItem>
{
    public void Configure(EntityTypeBuilder<TaskItem> builder)
    {
        builder.ToTable("Tasks");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
            .HasConversion(
                v => v.Id,
                v => new TaskId(v))
            .HasColumnName("Id");

        builder.OwnsOne(t => t.Title, title =>
        {
            title.Property(t => t.Title)
                .HasColumnName("Title")
                .HasMaxLength(200)
                .IsRequired();
        });

        builder.OwnsOne(t => t.Description, description =>
        {
            description.Property(d => d.Description)
                .HasColumnName("Description")
                .HasMaxLength(1000);
        });

        builder.OwnsOne(t => t.ColumnId, columnId =>
        {
            columnId.Property(c => c.ColumnId)
                .HasColumnName("ColumnId")
                .IsRequired();
        });

        builder.OwnsOne(t => t.Order, order =>
        {
            order.Property(o => o.Order)
                .HasColumnName("TaskOrder")
                .IsRequired();
        });

        builder.Property(t => t.CreatedAt)
            .HasColumnName("CreatedAt")
            .IsRequired();

        builder.Property(t => t.UpdatedAt)
            .HasColumnName("UpdatedAt")
            .IsRequired();
    }
}