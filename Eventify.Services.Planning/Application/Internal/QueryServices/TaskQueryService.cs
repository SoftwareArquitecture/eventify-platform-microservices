using Eventify.Services.Planning.Domain.Model.Aggregates;
using Eventify.Services.Planning.Domain.Model.Queries;
using Eventify.Services.Planning.Domain.Repositories;
using Eventify.Services.Planning.Domain.Services;

namespace Eventify.Services.Planning.Application.Internal.QueryServices;

public class TaskQueryService : ITaskQueryService
{
    private readonly ITaskRepository _taskRepository;

    public TaskQueryService(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<IEnumerable<TaskItem>> Handle(GetAllTasksQuery query)
    {
        return await _taskRepository.FindAllAsync();
    }

    public async Task<TaskItem?> Handle(GetTaskByIdQuery query)
    {
        return await _taskRepository.FindByIdAsync(query.Id);
    }

    public async Task<IEnumerable<TaskItem>> Handle(GetTasksByColumnIdQuery query)
    {
        return await _taskRepository.FindByColumnIdOrderedAsync(query.ColumnId);
    }

    public async Task<object> Handle(GetTaskBoardQuery query)
    {
        // Crear estructura del tablero con las 3 columnas estándar
        var columns = new[]
        {
            new { id = 1, title = "Por hacer", boardId = query.BoardId, order = 1 },
            new { id = 2, title = "En progreso", boardId = query.BoardId, order = 2 },
            new { id = 3, title = "Completado", boardId = query.BoardId, order = 3 }
        };

        var board = new
        {
            id = query.BoardId,
            title = "Mi Tablero de Tareas",
            description = "Tablero principal de gestión de tareas",
            columns = new List<object>()
        };

        var boardColumns = new List<object>();

        foreach (var column in columns)
        {
            var tasks = await _taskRepository.FindByColumnIdOrderedAsync(column.id);
            var columnData = new
            {
                column.id,
                column.title,
                column.boardId,
                column.order,
                tasks = tasks.Select(t => new
                {
                    id = t.Id.Id,
                    title = t.Title.Title,
                    description = t.Description.Description,
                    columnId = t.ColumnId.ColumnId,
                    order = t.Order.Order,
                    createdAt = t.CreatedAt.ToString("O")
                }).ToArray()
            };
            boardColumns.Add(columnData);
        }

        return new
        {
            board.id,
            board.title,
            board.description,
            columns = boardColumns.ToArray()
        };
    }
}