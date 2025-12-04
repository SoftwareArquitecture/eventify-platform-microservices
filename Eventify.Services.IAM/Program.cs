using Eventify.Services.IAM.Application.ACL;
using Eventify.Services.IAM.Application.Internal.CommandServices;
using Eventify.Services.IAM.Application.Internal.OutboundServices;
using Eventify.Services.IAM.Application.Internal.QueryServices;
using Eventify.Services.IAM.Domain.Repositories;
using Eventify.Services.IAM.Domain.Services;
using Eventify.Services.IAM.Infrastructure.Configuration;
using Eventify.Services.IAM.Infrastructure.Hashing.BCrypt.Services;
using Eventify.Services.IAM.Infrastructure.Persistence.EFC.Configuration;
using Eventify.Services.IAM.Infrastructure.Persistence.EFC.Repositories;
using Eventify.Services.IAM.Infrastructure.Pipeline.Middleware.Extensions;
using Eventify.Services.IAM.Infrastructure.Tokens.JWT.Configuration;
using Eventify.Services.IAM.Infrastructure.Tokens.JWT.Services;
using Eventify.Services.IAM.Interfaces.ACL;
using Eventify.Services.IAM.Interfaces.ACL.Services;
using Eventify.Shared.Domain.Repositories;
using Eventify.Shared.Infrastructure.Persistence.EFC.Configuration;
using Eventify.Shared.Infrastructure.Persistence.EFC.Repositories;
using Eventify.Shared.Infrastructure.Interfaces.ASP.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("TokenSettings"));

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<IamDbContext>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
        .LogTo(Console.WriteLine, LogLevel.Information)
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors();
});

builder.Services.AddScoped<AppDbContext>(sp =>
    sp.GetRequiredService<IamDbContext>());

builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddControllers(options =>
{
    options.Conventions.Add(new KebabCaseRouteNamingConvention());
});
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Eventify.Services.IAM", Version = "v1" });
    c.EnableAnnotations();
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter JWT token (without 'Bearer' prefix)",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });
    c.OperationFilter<AuthorizeCheckOperationFilter>();
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

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserCommandService, UserCommandService>();
builder.Services.AddScoped<IUserQueryService, UserQueryService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IHashingService, HashingService>();
builder.Services.AddScoped<IIamContextFacade, IamContextFacade>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// HttpClient for Profiles service communication
builder.Services.AddHttpClient<IProfilesContextFacade, ProfilesContextFacade>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<IamDbContext>();
    context.Database.EnsureCreated();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("AllowAll");
app.UseHttpsRedirection();
// app.UseRequestAuthorization(); // Desactivado para testing
// app.UseAuthorization(); // Desactivado para testing
app.MapControllers();

app.Run();