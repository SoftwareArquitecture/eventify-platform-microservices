using Eventify.Services.Operation.Application.Internal.CommandServices;
using Eventify.Services.Operation.Application.Internal.QueryService;
using Eventify.Services.Operation.Domain.Repositories;
using Eventify.Services.Operation.Domain.Services;
using Eventify.Services.Operation.Infrastructure.Persistence.EFC.Configuration;
using Eventify.Services.Operation.Infrastructure.Persistence.EFC.Repositories;
using Eventify.Shared.Domain.Repositories;
using Eventify.Shared.Infrastructure.Persistence.EFC.Configuration;
using Eventify.Shared.Infrastructure.Persistence.EFC.Repositories;
using Eventify.Shared.Infrastructure.Interfaces.ASP.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Database Configuration
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<OperationDbContext>(options =>
{
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 21)))
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
builder.Services.AddControllers(options =>
{
    options.Conventions.Add(new KebabCaseRouteNamingConvention());
});
builder.Services.AddEndpointsApiExplorer();

// Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Eventify.Services.Operation", Version = "v1" });
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
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();

// Command Services
builder.Services.AddScoped<IReviewCommandService, ReviewCommandService>();

// Query Services
builder.Services.AddScoped<IReviewQueryService, ReviewQueryService>();

// Unit of Work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<OperationDbContext>();
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