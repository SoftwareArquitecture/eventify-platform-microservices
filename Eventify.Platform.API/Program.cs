using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System.Text;

// API Gateway - Routes requests to microservices
var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json",
                 optional: true,
                 reloadOnChange: true)
    .AddJsonFile("ocelot.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"ocelot.{builder.Environment.EnvironmentName}.json",
                 optional: true,
                 reloadOnChange: true)
    .AddEnvironmentVariables();

var tokenSettings = builder.Configuration.GetSection("TokenSettings");

var secretKey = tokenSettings["Secret"]
                ?? "esta_es_una_clave_secreta_muy_larga_para_desarrollo_local_12345";

var key = Encoding.UTF8.GetBytes(secretKey);

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddOcelot(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.SetIsOriginAllowed(_ => true)
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials()
              .WithExposedHeaders("Content-Disposition", "Authorization"));
});

var app = builder.Build();

var logger = app.Services.GetRequiredService<ILogger<Program>>();
var environment = builder.Environment.EnvironmentName;
logger.LogInformation("Ocelot configuration loaded: ocelot.{Environment}.json", environment);

app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();

await app.UseOcelot();

app.Run();
