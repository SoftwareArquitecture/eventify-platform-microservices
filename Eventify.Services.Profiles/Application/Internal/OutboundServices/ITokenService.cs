namespace Eventify.Services.Profiles.Application.Internal.OutboundServices;

public interface ITokenService
{
    Task<int?> ValidateToken(string token);
}