using System.Security.Authentication;
using Eventify.Services.IAM.Application.Internal.OutboundServices;
using Eventify.Services.IAM.Domain.Model.Queries;
using Eventify.Services.IAM.Domain.Services;
using Eventify.Services.IAM.Infrastructure.Pipeline.Middleware.Attributes;

namespace Eventify.Services.IAM.Infrastructure.Pipeline.Middleware.Components;

public class RequestAuthorizationMiddleware(RequestDelegate next, ILogger<RequestAuthorizationMiddleware> logger)
{
    private readonly ILogger<RequestAuthorizationMiddleware> _logger = logger;

    public async Task InvokeAsync(
        HttpContext context,
        IUserQueryService userQueryService,
        ITokenService tokenService)
    {
        _logger.LogInformation("Entering InvokeAsync");

        var endpoint = context.Request.HttpContext.GetEndpoint();

        var allowAnonymous = endpoint != null && endpoint.Metadata
            .Any(m => m.GetType() == typeof(AllowAnonymousAttribute));

        if (endpoint == null || allowAnonymous)
        {
            _logger.LogInformation("Skipping authorization");
            await next(context);
            return;
        }

        _logger.LogInformation("Entering authorization");
        var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();
        _logger.LogInformation($"Authorization header: {authHeader}");

        var token = authHeader?.Split(" ").Last();
        _logger.LogInformation($"Extracted token: {token}");

        if (token is null) throw new AuthenticationException("Null or invalid token");

        var userId = await tokenService.ValidateToken(token);

        if (userId is null) throw new AuthenticationException("Invalid token");

        var getUserByIdQuery = new GetUserByIdQuery(userId.Value);


        var user = await userQueryService.Handle(getUserByIdQuery);
        _logger.LogInformation("Successful authorization. Updating Context...");
        context.Items["User"] = user;
        _logger.LogInformation("Continuing with Middleware Pipeline");
        await next(context);
    }
}