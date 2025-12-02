using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// 1. Cargar configuración de Ocelot
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

// 2. Configurar Seguridad (JWT)
var tokenSettings = builder.Configuration.GetSection("TokenSettings");
// Nota: Usamos un valor por defecto si no está en config para que no falle al compilar,
// pero en ejecución lo tomará de las variables de entorno.
var secretKey = tokenSettings["Secret"] ?? "esta_es_una_clave_secreta_muy_larga_para_desarrollo_local_12345";
var key = Encoding.UTF8.GetBytes(secretKey);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
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

// 3. Agregar Ocelot
builder.Services.AddOcelot();

// 4. Configurar CORS (Para que el frontend no tenga problemas)
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

app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();

// 5. Activar Ocelot
await app.UseOcelot();

app.Run();
