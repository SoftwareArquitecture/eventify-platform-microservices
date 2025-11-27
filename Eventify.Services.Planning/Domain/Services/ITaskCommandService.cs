using Eventify.Services.Planning.Domain.Model.Aggregates;
using Eventify.Services.Planning.Domain.Model.Commands;

namespace Eventify.Services.Planning.Domain.Services;

public interface ITaskCommandService
{
    Task<TaskItem?> Handle(CreateTaskCommand command);
    Task<TaskItem?> Handle(UpdateTaskCommand command);
    Task<TaskItem?> Handle(DeleteTaskCommand command);
    Task<TaskItem?> Handle(MoveTaskCommand command);
}