using Eventify.Services.Planning.Application.ACL;
using Eventify.Services.Planning.Application.Internal.CommandServices;
using Eventify.Services.Planning.Application.Internal.OutboundServices;
using Eventify.Services.Planning.Application.Internal.QueryServices;
using Eventify.Services.Planning.Domain.Repositories;
using Eventify.Services.Planning.Domain.Services;
using Eventify.Services.Planning.Infrastructure.Persistence.EFC.Configuration;
using Eventify.Services.Planning.Infrastructure.Persistence.EFC.Repositories;
using Eventify.Services.Planning.Infrastructure.Tokens.JWT.Configuration;
using Eventify.Services.Planning.Infrastructure.Tokens.JWT.Services;
using Eventify.Services.Planning.Interfaces.ACL;
using Eventify.Shared.Domain.Repositories;
using Eventify.Shared.Infrastructure.Persistence.EFC.Configuration;
using Eventify.Shared.Infrastructure.Persistence.EFC.Repositories;
using Eventify.Shared.Infrastructure.Interfaces.ASP.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

// Planning Service - Handles social events, quotes, tasks, and dashboard
var builder = WebApplication.CreateBuilder(args);

// Database Configuration
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<PlanningDbContext>(options =>
{
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 21)))
        .LogTo(Console.WriteLine, LogLevel.Information)
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors();
});

// Register AppDbContext alias
builder.Services.AddScoped<AppDbContext>(sp =>
    sp.GetRequiredService<PlanningDbContext>());

// Routing
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Controllers
builder.Services.AddControllers(options =>
{
    options.Conventions.Add(new KebabCaseRouteNamingConvention());
});
builder.Services.AddEndpointsApiExplorer();

// Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Eventify.Services.Planning", Version = "v1" });
    c.EnableAnnotations();
});

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.SetIsOriginAllowed(_ => true)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()
            .WithExposedHeaders("Content-Disposition", "Authorization"));
});

// Repositories
builder.Services.AddScoped<IQuoteRepository, QuoteRepository>();
builder.Services.AddScoped<IServiceItemRepository, ServiceItemRepository>();
builder.Services.AddScoped<ISocialEventRepository, SocialEventRepository>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();

// Command Services
builder.Services.AddScoped<IQuoteCommandService, QuoteCommandService>();
builder.Services.AddScoped<IServiceItemCommandService, ServiceItemCommandService>();
builder.Services.AddScoped<ISocialEventCommandService, SocialEventCommandService>();
builder.Services.AddScoped<ITaskCommandService, TaskCommandService>();

// Query Services
builder.Services.AddScoped<IQuoteQueryService, QuoteQueryService>();
builder.Services.AddScoped<IServiceItemQueryService, ServiceItemQueryService>();
builder.Services.AddScoped<ISocialEventQueryService, SocialEventQueryService>();
builder.Services.AddScoped<ITaskQueryService, TaskQueryService>();

// ACL Facade
builder.Services.AddScoped<ISocialEventContextFacade, SocialEventContextFacade>();

// Unit of Work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Token Settings
builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("TokenSettings"));

// Token Service
builder.Services.AddScoped<ITokenService, TokenService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<PlanningDbContext>();
    context.Database.EnsureCreated();
}

// Middleware Pipeline
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();