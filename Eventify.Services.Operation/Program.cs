using Eventify.Services.Operation.Application.Internal.CommandServices;
using Eventify.Services.Operation.Application.Internal.QueryService;
using Eventify.Services.Operation.Domain.Repositories;
using Eventify.Services.Operation.Domain.Services;
using Eventify.Services.Operation.Infrastructure.Persistence.EFC.Configuration;
using Eventify.Services.Operation.Infrastructure.Persistence.EFC.Repositories;
using Eventify.Shared.Domain.Repositories;
using Eventify.Shared.Infrastructure.Persistence.EFC.Configuration;
using Eventify.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Database Configuration
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<OperationDbContext>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
        .LogTo(Console.WriteLine, LogLevel.Information)
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors();
});

// Register AppDbContext alias
builder.Services.AddScoped<AppDbContext>(sp =>
    sp.GetRequiredService<OperationDbContext>());

// Routing
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Controllers
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Eventify.Services.Operation", Version = "v1" });
    c.EnableAnnotations();
});

// Repositories
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();

// Command Services
builder.Services.AddScoped<IReviewCommandService, ReviewCommandService>();

// Query Services
builder.Services.AddScoped<IReviewQueryService, ReviewQueryService>();

// Unit of Work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();

// Database Initialization
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<OperationDbContext>();
    context.Database.EnsureCreated();
}

// Middleware Pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();