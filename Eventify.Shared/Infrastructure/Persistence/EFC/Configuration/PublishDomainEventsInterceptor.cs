using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Eventify.Shared.Infrastructure.Persistence.EFC.Configuration;

public class PublishDomainEventsInterceptor : SaveChangesInterceptor
{
    // Si usas MediatR para publicar eventos, inyéctalo aquí.
    // Por ahora, para que compile y funcione el TF, puedes dejarlo vacío
    // o implementar la lógica básica si la tenías en el monolito.

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        // Aquí iría la lógica para publicar eventos de dominio antes de guardar
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
        InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        // Aquí iría la lógica asíncrona
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}