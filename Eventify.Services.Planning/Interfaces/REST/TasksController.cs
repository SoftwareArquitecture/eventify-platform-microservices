using System.Net.Mime;
using Eventify.Services.Planning.Domain.Model.Commands;
using Eventify.Services.Planning.Domain.Model.Queries;
using Eventify.Services.Planning.Domain.Services;
using Eventify.Services.Planning.Interfaces.REST.Resources;
using Eventify.Services.Planning.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Eventify.Services.Planning.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Task Management Endpoints.")]
public class TasksController : ControllerBase
{
    private readonly ILogger<TasksController> _logger;
    private readonly ITaskCommandService _taskCommandService;
    private readonly ITaskQueryService _taskQueryService;

    public TasksController(
        ITaskCommandService taskCommandService,
        ITaskQueryService taskQueryService,
        ILogger<TasksController> logger)
    {
        _taskCommandService = taskCommandService;
        _taskQueryService = taskQueryService;
        _logger = logger;
    }

    [HttpGet("boards/{boardId}")]
    [SwaggerOperation(
        Summary = "Gets a task board with its columns and tasks",
        Description = "Gets a complete task board including columns and tasks",
        OperationId = "GetTaskBoard")]
    [SwaggerResponse(200, "The task board was found")]
    [SwaggerResponse(404, "The task board was not found")]
    public async Task<IActionResult> GetTaskBoard(int boardId)
    {
        try
        {
            _logger.LogInformation("=== GetTaskBoard - Start ===");
            _logger.LogInformation("Received boardId: {BoardId}", boardId);

            var query = new GetTaskBoardQuery(boardId);
            var board = await _taskQueryService.Handle(query);

            _logger.LogInformation("=== GetTaskBoard - Success ===");
            return Ok(board);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "=== GetTaskBoard - Error: {Message} ===", ex.Message);
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPost("tasks")]
    [SwaggerOperation(
        Summary = "Creates a new task",
        Description = "Creates a new task in the specified column",
        OperationId = "CreateTask")]
    [SwaggerResponse(201, "The task was created")]
    [SwaggerResponse(400, "The task was not created")]
    public async Task<IActionResult> CreateTask([FromBody] CreateTaskResource resource)
    {
        try
        {
            _logger.LogInformation("=== CreateTask - Start ===");
            _logger.LogInformation("Received resource: {@Resource}", resource);

            var command = CreateTaskCommandFromResourceAssembler.ToCommandFromResource(resource);
            var task = await _taskCommandService.Handle(command);

            if (task is null)
            {
                _logger.LogWarning("Service returned null, returning BadRequest");
                return BadRequest("Task could not be created");
            }

            var taskResource = TaskResourceFromEntityAssembler.ToResourceFromEntity(task);
            _logger.LogInformation("=== CreateTask - Success with ID {TaskId} ===", task.Id);

            return CreatedAtAction(nameof(GetTask), new { id = task.Id }, taskResource);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "=== CreateTask - Error: {Message} ===", ex.Message);
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet("tasks/{id}")]
    [SwaggerOperation(
        Summary = "Gets a task by ID",
        Description = "Gets a specific task by its ID",
        OperationId = "GetTask")]
    [SwaggerResponse(200, "The task was found")]
    [SwaggerResponse(404, "The task was not found")]
    public async Task<IActionResult> GetTask(int id)
    {
        try
        {
            _logger.LogInformation("=== GetTask - Start ===");
            _logger.LogInformation("Received taskId: {TaskId}", id);

            var query = new GetTaskByIdQuery(id);
            var task = await _taskQueryService.Handle(query);

            if (task == null)
            {
                _logger.LogWarning("Task not found for ID: {TaskId}", id);
                return NotFound();
            }

            var taskResource = TaskResourceFromEntityAssembler.ToResourceFromEntity(task);
            _logger.LogInformation("=== GetTask - Success ===");
            return Ok(taskResource);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "=== GetTask - Error: {Message} ===", ex.Message);
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPut("tasks/{id}")]
    [SwaggerOperation(
        Summary = "Updates a task",
        Description = "Updates an existing task",
        OperationId = "UpdateTask")]
    [SwaggerResponse(200, "The task was updated")]
    [SwaggerResponse(404, "The task was not found")]
    [SwaggerResponse(400, "The task was not updated")]
    public async Task<IActionResult> UpdateTask(int id, [FromBody] UpdateTaskResource resource)
    {
        try
        {
            _logger.LogInformation("=== UpdateTask - Start ===");
            _logger.LogInformation("Received taskId: {TaskId}", id);
            _logger.LogInformation("Received resource: {@Resource}", resource);

            _logger.LogInformation("=== UpdateTask - Creating command ===");
            var command = UpdateTaskCommandFromResourceAssembler.ToCommandFromResource(id, resource);
            _logger.LogInformation("Created command: {@Command}", command);

            _logger.LogInformation("=== UpdateTask - Calling command service ===");
            var task = await _taskCommandService.Handle(command);
            _logger.LogInformation("Command service returned: {TaskResult}",
                task != null ? "Task found" : "Task not found");

            if (task is null)
            {
                _logger.LogWarning("Task not found for ID: {TaskId}", id);
                return NotFound();
            }

            _logger.LogInformation("=== UpdateTask - Creating response resource ===");
            var taskResource = TaskResourceFromEntityAssembler.ToResourceFromEntity(task);
            _logger.LogInformation("=== UpdateTask - Success ===");
            return Ok(taskResource);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "=== UpdateTask - Error: {Message} - StackTrace: {StackTrace} ===", ex.Message,
                ex.StackTrace);
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpDelete("tasks/{id}")]
    [SwaggerOperation(
        Summary = "Deletes a task",
        Description = "Deletes a task by its ID",
        OperationId = "DeleteTask")]
    [SwaggerResponse(204, "The task was deleted")]
    [SwaggerResponse(404, "The task was not found")]
    public async Task<IActionResult> DeleteTask(int id)
    {
        try
        {
            _logger.LogInformation("=== DeleteTask - Start ===");
            _logger.LogInformation("Received taskId: {TaskId}", id);

            var command = new DeleteTaskCommand(id);
            var task = await _taskCommandService.Handle(command);

            if (task is null)
            {
                _logger.LogWarning("Task not found for ID: {TaskId}", id);
                return NotFound();
            }

            _logger.LogInformation("=== DeleteTask - Success ===");
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "=== DeleteTask - Error: {Message} ===", ex.Message);
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPut("tasks/{id}/move")]
    [SwaggerOperation(
        Summary = "Moves a task between columns",
        Description = "Moves a task from one column to another",
        OperationId = "MoveTask")]
    [SwaggerResponse(200, "The task was moved")]
    [SwaggerResponse(404, "The task was not found")]
    [SwaggerResponse(400, "The task was not moved")]
    public async Task<IActionResult> MoveTask(int id, [FromBody] MoveTaskResource resource)
    {
        try
        {
            _logger.LogInformation("=== MoveTask - Start ===");
            _logger.LogInformation("Received taskId: {TaskId}", id);
            _logger.LogInformation("Received resource: {@Resource}", resource);

            var command = MoveTaskCommandFromResourceAssembler.ToCommandFromResource(id, resource);
            var task = await _taskCommandService.Handle(command);

            if (task is null)
            {
                _logger.LogWarning("Task not found for ID: {TaskId}", id);
                return NotFound();
            }

            var taskResource = TaskResourceFromEntityAssembler.ToResourceFromEntity(task);
            _logger.LogInformation("=== MoveTask - Success ===");
            return Ok(taskResource);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "=== MoveTask - Error: {Message} ===", ex.Message);
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}