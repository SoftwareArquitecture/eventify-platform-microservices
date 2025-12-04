using Eventify.Services.Profiles.Application.ACL;
using Eventify.Services.Profiles.Application.Internal.CommandServices;
using Eventify.Services.Profiles.Application.Internal.OutboundServices;
using Eventify.Services.Profiles.Application.Internal.QueryServices;
using Eventify.Services.Profiles.Domain.Repositories;
using Eventify.Services.Profiles.Domain.Services;
using Eventify.Services.Profiles.Infrastructure.Persistence.EFC.Configuration;
using Eventify.Services.Profiles.Infrastructure.Persistence.EFC.Repositories;
using Eventify.Services.Profiles.Infrastructure.Tokens.JWT.Configuration;
using Eventify.Services.Profiles.Infrastructure.Tokens.JWT.Services;
using Eventify.Services.Profiles.Interfaces.ACL;
using Eventify.Shared.Domain.Repositories;
using Eventify.Shared.Infrastructure.Persistence.EFC.Configuration;
using Eventify.Shared.Infrastructure.Persistence.EFC.Repositories;
using Eventify.Shared.Infrastructure.Interfaces.ASP.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Database Configuration
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ProfilesDbContext>(options =>
{
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 21)))
        .LogTo(Console.WriteLine, LogLevel.Information)
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors();
});

// Register AppDbContext alias
builder.Services.AddScoped<AppDbContext>(sp =>
    sp.GetRequiredService<ProfilesDbContext>());

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
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Eventify.Services.Profiles", Version = "v1" });
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
builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
builder.Services.AddScoped<IAlbumRepository, AlbumRepository>();
builder.Services.AddScoped<IServiceCatalogRepository, ServiceCatalogRepository>();

// Command Services
builder.Services.AddScoped<IProfileCommandService, ProfileCommandService>();
builder.Services.AddScoped<IAlbumCommandService, AlbumCommandService>();
builder.Services.AddScoped<IServiceCatalogCommandService, ServiceCatalogCommandService>();

// Query Services
builder.Services.AddScoped<IProfileQueryService, ProfileQueryService>();
builder.Services.AddScoped<IAlbumQueryService, AlbumQueryService>();
builder.Services.AddScoped<IServiceCatalogQueryService, ServiceCatalogQueryService>();

// ACL Facade
builder.Services.AddScoped<IProfilesContextFacade, ProfilesContextFacade>();

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
    var context = services.GetRequiredService<ProfilesDbContext>();
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