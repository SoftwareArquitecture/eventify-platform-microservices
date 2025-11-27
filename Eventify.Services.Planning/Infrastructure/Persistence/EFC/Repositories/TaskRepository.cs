using Eventify.Services.Planning.Domain.Model.Aggregates;
using Eventify.Services.Planning.Domain.Repositories;
using Eventify.Shared.Infrastructure.Persistence.EFC.Configuration;
using Eventify.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Eventify.Services.Planning.Infrastructure.Persistence.EFC.Repositories;

public class TaskRepository : BaseRepository<TaskItem>, ITaskRepository
{
    public TaskRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<TaskItem>> FindAllAsync()
    {
        return await Context.Set<TaskItem>().ToListAsync();
    }

    public new async Task<TaskItem?> FindByIdAsync(int id)
    {
        var allTasks = await Context.Set<TaskItem>().ToListAsync();
        return allTasks.FirstOrDefault(t => t.Id.Id == id);
    }

    public async Task<IEnumerable<TaskItem>> FindByColumnIdAsync(int columnId)
    {
        var allTasks = await Context.Set<TaskItem>().ToListAsync();
        return allTasks.Where(t => t.ColumnId.ColumnId == columnId);
    }

    public async Task<IEnumerable<TaskItem>> FindByColumnIdOrderedAsync(int columnId)
    {
        var allTasks = await Context.Set<TaskItem>().ToListAsync();
        return allTasks
            .Where(t => t.ColumnId.ColumnId == columnId)
            .OrderBy(t => t.Order.Order);
    }
}