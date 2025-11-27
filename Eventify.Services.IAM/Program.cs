using Eventify.Services.IAM.Application.Internal.CommandServices;
using Eventify.Services.IAM.Application.Internal.OutboundServices;
using Eventify.Services.IAM.Application.Internal.QueryServices;
using Eventify.Services.IAM.Domain.Repositories;
using Eventify.Services.IAM.Domain.Services;
using Eventify.Services.IAM.Infrastructure.Hashing.BCrypt.Services;
using Eventify.Services.IAM.Infrastructure.Persistence.EFC.Configuration;
using Eventify.Services.IAM.Infrastructure.Persistence.EFC.Repositories;
using Eventify.Services.IAM.Infrastructure.Tokens.JWT.Services;
using Eventify.Services.IAM.Interfaces.ACL;
using Eventify.Services.IAM.Interfaces.ACL.Services;
using Eventify.Shared.Domain.Repositories;
using Eventify.Shared.Infrastructure.Persistence.EFC.Configuration;
using Eventify.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

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

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Eventify.Services.IAM", Version = "v1" });
    c.EnableAnnotations();
});

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserCommandService, UserCommandService>();
builder.Services.AddScoped<IUserQueryService, UserQueryService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IHashingService, HashingService>();
builder.Services.AddScoped<IIamContextFacade, IamContextFacade>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<IamDbContext>();
    context.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();