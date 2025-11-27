using Eventify.Services.Planning.Domain.Model.Aggregates;
using Eventify.Services.Planning.Domain.Model.Commands;
using Eventify.Services.Planning.Domain.Model.ValueObjects;
using Eventify.Services.Planning.Domain.Repositories;
using Eventify.Services.Planning.Domain.Services;
using Eventify.Shared.Domain.Repositories;

namespace Eventify.Services.Planning.Application.Internal.CommandServices;

public class TaskCommandService : ITaskCommandService
{
    private readonly ILogger<TaskCommandService> _logger;
    private readonly ITaskRepository _taskRepository;
    private readonly IUnitOfWork _unitOfWork;

    public TaskCommandService(ITaskRepository taskRepository, IUnitOfWork unitOfWork,
        ILogger<TaskCommandService> logger)
    {
        _taskRepository = taskRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<TaskItem?> Handle(CreateTaskCommand command)
    {
        var task = new TaskItem(
            new TaskTitle(command.Title),
            new TaskDescription(command.Description),
            new TaskColumnId(command.ColumnId),
            new TaskOrder(command.Order)
        );

        await _taskRepository.AddAsync(task);
        await _unitOfWork.CompleteAsync();
        return task;
    }

    public async Task<TaskItem?> Handle(UpdateTaskCommand command)
    {
        try
        {
            _logger.LogInformation("=== TaskCommandService.Handle(UpdateTaskCommand) - Start ===");
            _logger.LogInformation("Command: {@Command}", command);

            _logger.LogInformation("=== Searching for task with ID: {TaskId} ===", command.Id);
            var task = await _taskRepository.FindByIdAsync(command.Id);
            _logger.LogInformation("Task found: {TaskFound}", task != null);

            if (task == null)
            {
                _logger.LogWarning("Task with ID {TaskId} not found", command.Id);
                return null;
            }

            _logger.LogInformation("=== Updating task information ===");
            _logger.LogInformation(
                "Current task: Title='{CurrentTitle}', Description='{CurrentDescription}', ColumnId={CurrentColumnId}, Order={CurrentOrder}",
                task.Title.Title, task.Description.Description, task.ColumnId.ColumnId, task.Order.Order);

            task.UpdateInformation(
                new TaskTitle(command.Title),
                new TaskDescription(command.Description),
                new TaskColumnId(command.ColumnId),
                new TaskOrder(command.Order)
            );

            _logger.LogInformation(
                "Updated task: Title='{NewTitle}', Description='{NewDescription}', ColumnId={NewColumnId}, Order={NewOrder}",
                command.Title, command.Description, command.ColumnId, command.Order);

            _logger.LogInformation("=== Updating task in repository ===");
            _taskRepository.Update(task);

            _logger.LogInformation("=== Completing unit of work ===");
            await _unitOfWork.CompleteAsync();

            _logger.LogInformation("=== TaskCommandService.Handle(UpdateTaskCommand) - Success ===");
            return task;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "=== TaskCommandService.Handle(UpdateTaskCommand) - Error: {Message} - StackTrace: {StackTrace} ===",
                ex.Message, ex.StackTrace);
            throw;
        }
    }

    public async Task<TaskItem?> Handle(DeleteTaskCommand command)
    {
        var task = await _taskRepository.FindByIdAsync(command.Id);
        if (task == null) return null;

        _taskRepository.Remove(task);
        await _unitOfWork.CompleteAsync();
        return task;
    }

    public async Task<TaskItem?> Handle(MoveTaskCommand command)
    {
        var task = await _taskRepository.FindByIdAsync(command.Id);
        if (task == null) return null;

        task.MoveToColumn(
            new TaskColumnId(command.TargetColumnId),
            new TaskOrder(command.Order)
        );

        _taskRepository.Update(task);
        await _unitOfWork.CompleteAsync();
        return task;
    }
}