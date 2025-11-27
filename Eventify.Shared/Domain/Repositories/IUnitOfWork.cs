namespace Eventify.Shared.Domain.Repositories;

public interface IUnitOfWork
{
    Task CompleteAsync();
}