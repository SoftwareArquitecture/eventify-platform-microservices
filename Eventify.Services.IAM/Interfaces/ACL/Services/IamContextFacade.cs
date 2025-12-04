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
        string email,
        string firstName,
        string lastName,
        string phoneNumber,
        string street,
        string city,
        string country,
        string? role = null)
    {
        var signUpCommand = new SignUpCommand(
            username,
            password,
            email,
            firstName,
            lastName,
            phoneNumber,
            street,
            city,
            country,
            role);
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