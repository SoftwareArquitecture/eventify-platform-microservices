using Eventify.Services.IAM.Domain.Model.Aggregates;
using Eventify.Shared.Infrastructure.Persistence.EFC.Configuration;
using Eventify.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Eventify.Services.IAM.Infrastructure.Persistence.EFC.Configuration;

public class IamDbContext(DbContextOptions options)
    : AppDbContext(options) // Hereda de tu AppDbContext del Shared si tiene la config base
{
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        base.OnConfiguring(builder);
        // Aquí activas el interceptor de auditoría si lo usas
        builder.AddInterceptors(new PublishDomainEventsInterceptor());
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Aquí importas SOLO la configuración de tablas de IAM
        // (Asegúrate de tener la clase ModelBuilderExtensions dentro de tu carpeta IAM movida)
        builder.UseSnakeCaseNamingConvention();

        // Configura tus entidades de IAM (User, Roles, etc)
        builder.Entity<User>().ToTable("Users");
        builder.Entity<User>().HasKey(u => u.Id);
        builder.Entity<User>().Property(u => u.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<User>().Property(u => u.Username).IsRequired();
        // ... O usa tus extensiones: builder.ApplyConfiguration(new UserConfiguration());
    }
}