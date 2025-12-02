using Eventify.Services.IAM.Domain.Model.Commands;
using Eventify.Services.IAM.Domain.Model.Queries;
using Eventify.Services.IAM.Domain.Services;

namespace Eventify.Services.IAM.Interfaces.ACL.Services;

public class IamContextFacade(IUserCommandService userCommandService, IUserQueryService userQueryService)
    : IIamContextFacade
{
    public async Task<int> CreateUser(
        string username,
        string password,
        string firstName,
        string lastName,
        string email,
        string phoneNumber,
        string role,
        string? street = null,
        string? number = null,
        string? city = null,
        string? postalCode = null,
        string? country = null,
        string? webSite = null,
        string? biography = null)
    {
        var signUpCommand = new SignUpCommand(
            username,
            password,
            firstName,
            lastName,
            email,
            phoneNumber,
            role,
            street,
            number,
            city,
            postalCode,
            country,
            webSite,
            biography);
        await userCommandService.Handle(signUpCommand);
        var getUserByUsernameQuery = new GetUserByUsernameQuery(username);
        var result = await userQueryService.Handle(getUserByUsernameQuery);
        return result?.Id ?? 0;
    }

    public async Task<int> FetchUserIdByUsername(string username)
    {
        var getUserByUsernameQuery = new GetUserByUsernameQuery(username);
        var result = await userQueryService.Handle(getUserByUsernameQuery);
        return result?.Id ?? 0;
    }

    public async Task<string> FetchUsernameByUserId(int userId)
    {
        var getUserByIdQuery = new GetUserByIdQuery(userId);
        var result = await userQueryService.Handle(getUserByIdQuery);
        return result?.Username ?? string.Empty;
    }
}