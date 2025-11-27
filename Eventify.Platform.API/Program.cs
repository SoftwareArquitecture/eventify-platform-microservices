using Eventify.Platform.API.IAM.Application.Internal.CommandServices;
using Eventify.Platform.API.IAM.Application.Internal.OutboundServices;
using Eventify.Platform.API.IAM.Application.Internal.QueryServices;
using Eventify.Platform.API.IAM.Domain.Repositories;
using Eventify.Platform.API.IAM.Domain.Services;
using Eventify.Platform.API.IAM.Infrastructure.Hashing.BCrypt.Services;
using Eventify.Platform.API.IAM.Infrastructure.Persistence.EFC.Repositories;
using Eventify.Platform.API.IAM.Infrastructure.Pipeline.Middleware.Extensions;
using Eventify.Platform.API.IAM.Infrastructure.Tokens.JWT.Configuration;
using Eventify.Platform.API.IAM.Infrastructure.Tokens.JWT.Services;
using Eventify.Platform.API.IAM.Interfaces.ACL;
using Eventify.Platform.API.IAM.Interfaces.ACL.Services;
using Eventify.Platform.API.Operation.Application.Internal.CommandServices;
using Eventify.Platform.API.Operation.Application.Internal.QueryService;
using Eventify.Platform.API.Operation.Domain.Repositories;
using Eventify.Platform.API.Operation.Domain.Services;
using Eventify.Platform.API.Operation.infrastructure.Persistence.EFC.Repositories;
using Eventify.Platform.API.Planning.Application.ACL;
using Eventify.Platform.API.Planning.Application.Internal.CommandServices;
using Eventify.Platform.API.Planning.Application.Internal.QueryServices;
using Eventify.Platform.API.Planning.Domain.Repositories;
using Eventify.Platform.API.Planning.Domain.Services;
using Eventify.Platform.API.Planning.Infrastructure.Persistence.EFC.Repositories;
using Eventify.Platform.API.Profiles.Application.ACL;
using Eventify.Platform.API.Profiles.Application.Internal.CommandServices;
using Eventify.Platform.API.Profiles.Application.Internal.QueryServices;
using Eventify.Platform.API.Profiles.Domain.Repositories;
using Eventify.Platform.API.Profiles.Domain.Services;
using Eventify.Platform.API.Profiles.Infrastructure.Persistence.EFC.Repositories;
using Eventify.Platform.API.Profiles.Interfaces.ACL;
using Eventify.Platform.API.Planning.Interfaces.ACL;
using Eventify.Platform.API.Shared.Domain.Repositories;
using Eventify.Platform.API.Shared.Infrastructure.Interfaces.ASP.Configuration;
using Eventify.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using Eventify.Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

using Microsoft.OpenApi.Models;




var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add ASP.NET Core MVC with kebab Case Route Naming Convention
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers(options => options.Conventions.Add(new KebabCaseRouteNamingConvention()));
builder.Services.AddEndpointsApiExplorer();

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllPolicy",
        policy => policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

// Add Configuration For Entity Framework Core
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (connectionString == null) throw new InvalidOperationException("Connection string not found.");

// Configure Database Context and Logging Levels
if (builder.Environment.IsDevelopment())
    builder.Services.AddDbContext<AppDbContext>(
        options =>
        {
            options.UseMySQL(connectionString)
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
        });
else if (builder.Environment.IsProduction())
    builder.Services.AddDbContext<AppDbContext>(
        options =>
        {
            options.UseMySQL(connectionString)
                .LogTo(Console.WriteLine, LogLevel.Error)
                .EnableDetailedErrors();
        });

// Add Swagger/OpenAPI support
builder.Services.AddSwaggerGen(options => {
    // General API Information
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "AngelDevs.Eventify.Platform.API",
        Version = "v1",
        Description = "Eventify Platform API",
        TermsOfService = new Uri("https://angeldevs.eventify.com/tos"),
        Contact = new OpenApiContact
        {
            Name = "AngelDevs",
            Email = "contact.eventify@angeldevs.com"
        },
        License = new OpenApiLicense
        {
            Name = "Apache 2.0",
            Url = new Uri("https://www.apache.org/licenses/LICENSE-2.0.html")
        },
    });
    options.EnableAnnotations();
    
    // Add Bearer Authentication for Swagger
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    // Add Security Requirement for Swagger
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            Array.Empty<string>()
        }
    });
    
});

// Dependency Injection

// Shared Bounded Context
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Planning Bounded Context

builder.Services.AddScoped<IQuoteRepository, QuoteRepository>();
builder.Services.AddScoped<IQuoteCommandService, QuoteCommandService>();
builder.Services.AddScoped<IQuoteQueryService, QuoteQueryService>();

builder.Services.AddScoped<IServiceItemRepository, ServiceItemRepository>();
builder.Services.AddScoped<IServiceItemCommandService, ServiceItemCommandService>();
builder.Services.AddScoped<IServiceItemQueryService, ServiceItemQueryService>();

// SocialEvents Bounded Context - Dependency Injection
builder.Services.AddScoped<ISocialEventRepository, SocialEventRepository>();
builder.Services.AddScoped<ISocialEventCommandService, SocialEventCommandService>();
builder.Services.AddScoped<ISocialEventQueryService, SocialEventQueryService>();

// Task Management - Dependency Injection
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ITaskCommandService, TaskCommandService>();
builder.Services.AddScoped<ITaskQueryService, TaskQueryService>();

// Shared Bounded Context
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


// Profiles Bounded Context
builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
builder.Services.AddScoped<IProfileCommandService, ProfileCommandService>();
builder.Services.AddScoped<IProfileQueryService, ProfileQueryService>();
builder.Services.AddScoped<IProfilesContextFacade, ProfilesContextFacade>();

builder.Services.AddScoped<IAlbumRepository, AlbumRepository>();
builder.Services.AddScoped<IAlbumCommandService, AlbumCommandService>();
builder.Services.AddScoped<IAlbumQueryService, AlbumQueryService>();

builder.Services.AddScoped<IServiceCatalogRepository, ServiceCatalogRepository>();
builder.Services.AddScoped<IServiceCatalogCommandService, ServiceCatalogCommandService>();
builder.Services.AddScoped<IServiceCatalogQueryService, ServiceCatalogQueryService>();

// Operation Bounded Context
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IReviewCommandService, ReviewCommandService>();
builder.Services.AddScoped<IReviewQueryService, ReviewQueryService>();

// IAM Bounded Context

// TokenSettings Configuration

builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("TokenSettings"));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserCommandService, UserCommandService>();
builder.Services.AddScoped<IUserQueryService, UserQueryService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IHashingService, HashingService>();
builder.Services.AddScoped<IIamContextFacade, IamContextFacade>();

//builder.Services.AddScoped(typeof(ICommandPipelineBehavior<>), typeof(LoggingCommandBehavior<>));

var app = builder.Build();


// Configure SocialEvents model at startup
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    
    // Ensure database is created with SocialEvents configuration
    context.Database.EnsureCreated();
 
}

// Use Swagger for API documentation if in development mode

    app.UseSwagger();
    app.UseSwaggerUI();
// Apply CORS Policy
app.UseCors("AllowAllPolicy");

// Configure the IAM HTTP request pipeline.
app.UseRequestAuthorization();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

