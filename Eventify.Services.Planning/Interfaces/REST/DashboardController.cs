using System.Net.Mime;
using Eventify.Services.Planning.Domain.Model.ValueObjects;
using Eventify.Services.Planning.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Eventify.Services.Planning.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Dashboard statistics endpoints")]
public class DashboardController : ControllerBase
{
    private readonly ITaskRepository _taskRepository;
    private readonly IQuoteRepository _quoteRepository;
    private readonly ISocialEventRepository _socialEventRepository;
    private readonly ILogger<DashboardController> _logger;

    public DashboardController(
        ITaskRepository taskRepository,
        IQuoteRepository quoteRepository,
        ISocialEventRepository socialEventRepository,
        ILogger<DashboardController> logger)
    {
        _taskRepository = taskRepository;
        _quoteRepository = quoteRepository;
        _socialEventRepository = socialEventRepository;
        _logger = logger;
    }

    [HttpGet("statistics/organizers/{organizerId}")]
    [SwaggerOperation(
        Summary = "Get dashboard statistics for an organizer",
        Description = "Returns statistics including pending tasks, pending quotes, upcoming events, and completed events",
        OperationId = "GetDashboardStatistics")]
    [SwaggerResponse(200, "Statistics retrieved successfully")]
    [SwaggerResponse(500, "Internal server error")]
    public async Task<IActionResult> GetDashboardStatistics(int organizerId)
    {
        try
        {
            _logger.LogInformation("Getting dashboard statistics for organizer {OrganizerId}", organizerId);

            // Get all quotes for the organizer
            var quotes = await _quoteRepository.FindByOrganizerIdAsync(new OrganizerId(organizerId));
            var pendingQuotesCount = quotes.Count(q => q.Status == EQuoteStatus.Pending);

            // Get all tasks from column 1 (Por hacer)
            var pendingTasks = await _taskRepository.FindByColumnIdAsync(1);
            var pendingTasksCount = pendingTasks.Count();

            // Get all social events
            var allEvents = (await _socialEventRepository.ListAsync()).ToList();
            var today = DateTime.Today;

            // Upcoming events (not completed and date >= today)
            var upcomingEventsCount = allEvents.Count(e =>
                e.Status != EStatusType.Completed &&
                e.EventDate.Date >= today);

            // Completed events
            var completedEventsCount = allEvents.Count(e =>
                e.Status == EStatusType.Completed);

            var statistics = new
            {
                pendingTasks = pendingTasksCount,
                pendingQuotes = pendingQuotesCount,
                upcomingEvents = upcomingEventsCount,
                completedEvents = completedEventsCount
            };

            _logger.LogInformation("Dashboard statistics: {@Statistics}", statistics);
            return Ok(statistics);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting dashboard statistics for organizer {OrganizerId}", organizerId);
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet("activity/organizers/{organizerId}")]
    [SwaggerOperation(
        Summary = "Get recent activity for an organizer",
        Description = "Returns recent tasks, quotes, and events (max 10 items combined)",
        OperationId = "GetRecentActivity")]
    [SwaggerResponse(200, "Activity retrieved successfully")]
    [SwaggerResponse(500, "Internal server error")]
    public async Task<IActionResult> GetRecentActivity(int organizerId)
    {
        try
        {
            _logger.LogInformation("Getting recent activity for organizer {OrganizerId}", organizerId);

            // Get recent quotes (last 5)
            var quotes = await _quoteRepository.FindByOrganizerIdAsync(new OrganizerId(organizerId));
            var recentQuotes = quotes
                .OrderByDescending(q => q.EventDate)
                .Take(5)
                .Select(q => new
                {
                    type = "quote",
                    title = q.Title,
                    description = $"Cotización para {q.GuestQuantity} invitados",
                    timestamp = q.EventDate,
                    status = q.Status.ToString()
                });

            // Get recent events (last 5)
            var events = (await _socialEventRepository.ListAsync()).ToList();
            var recentEvents = events
                .OrderByDescending(e => e.EventDate.Date)
                .Take(5)
                .Select(e => new
                {
                    type = "event",
                    title = e.Title.Title,
                    description = $"Evento en {e.Place.Place}",
                    timestamp = e.EventDate.Date,
                    status = e.Status.ToString()
                });

            // Get recent tasks (last 5)
            var tasks = (await _taskRepository.FindAllAsync()).ToList();
            var recentTasks = tasks
                .OrderByDescending(t => t.CreatedAt)
                .Take(5)
                .Select(t => new
                {
                    type = "task",
                    title = t.Title.Title,
                    description = t.Description.Description,
                    timestamp = t.CreatedAt,
                    status = t.ColumnId.ColumnId == 3 ? "Completed" : t.ColumnId.ColumnId == 2 ? "InProgress" : "Pending"
                });

            // Combine all activities and order by timestamp, take top 10
            var allActivity = recentQuotes
                .Concat(recentEvents.Cast<dynamic>())
                .Concat(recentTasks.Cast<dynamic>())
                .OrderByDescending(a => a.timestamp)
                .Take(10)
                .ToList();

            _logger.LogInformation("Found {Count} recent activities", allActivity.Count);
            return Ok(allActivity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting recent activity for organizer {OrganizerId}", organizerId);
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}
