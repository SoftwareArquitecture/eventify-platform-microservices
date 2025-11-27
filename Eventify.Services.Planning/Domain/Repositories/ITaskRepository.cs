using Eventify.Services.Planning.Domain.Model.Aggregates;
using Eventify.Shared.Domain.Repositories;

namespace Eventify.Services.Planning.Domain.Repositories;

public interface ITaskRepository : IBaseRepository<TaskItem>
{
    Task<IEnumerable<TaskItem>> FindAllAsync();
    new Task<TaskItem?> FindByIdAsync(int id);
    Task<IEnumerable<TaskItem>> FindByColumnIdAsync(int columnId);
    Task<IEnumerable<TaskItem>> FindByColumnIdOrderedAsync(int columnId);
}