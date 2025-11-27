namespace Eventify.Services.Planning.Application.Internal.OutboundServices;

public interface ITokenService
{
    Task<int?> ValidateToken(string token);
}