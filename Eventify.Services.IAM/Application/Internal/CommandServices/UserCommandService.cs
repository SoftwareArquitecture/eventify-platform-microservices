using Eventify.Services.IAM.Application.ACL;
using Eventify.Services.IAM.Application.Internal.OutboundServices;
using Eventify.Services.IAM.Domain.Model.Aggregates;
using Eventify.Services.IAM.Domain.Model.Commands;
using Eventify.Services.IAM.Domain.Repositories;
using Eventify.Services.IAM.Domain.Services;
using Eventify.Shared.Domain.Repositories;

namespace Eventify.Services.IAM.Application.Internal.CommandServices;

/**
 * <summary>
 *     The user command service
 * </summary>
 * <remarks>
 *     This class is used to handle user commands
 * </remarks>
 */
public class UserCommandService(
    IUserRepository userRepository,
    ITokenService tokenService,
    IHashingService hashingService,
    IUnitOfWork unitOfWork,
    IProfilesContextFacade profilesContextFacade)
    : IUserCommandService
{
    /**
     * <summary>
     *     Handle sign in command
     * </summary>
     * <param name="command">The sign in command</param>
     * <returns>The authenticated user and the JWT token</returns>
     */
    public async Task<(User user, string token)> Handle(SignInCommand command)
    {
        var user = await userRepository.FindByUsernameAsync(command.Username);

        if (user == null || !hashingService.VerifyPassword(command.Password, user.PasswordHash))
            throw new Exception("Invalid username or password");

        var token = tokenService.GenerateToken(user);

        return (user, token);
    }

    /**
     * <summary>
     *     Handle sign up command
     * </summary>
     * <param name="command">The sign up command</param>
     * <returns>A confirmation message on successful creation.</returns>
     */
    public async Task Handle(SignUpCommand command)
    {
        if (userRepository.ExistsByUsername(command.Username))
            throw new Exception($"Username {command.Username} is already taken");

        var hashedPassword = hashingService.HashPassword(command.Password);
        var user = new User(command.Username, hashedPassword);
        try
        {
            await userRepository.AddAsync(user);
            await unitOfWork.CompleteAsync();

            // Create profile in Profiles service
            await profilesContextFacade.CreateProfile(
                user.Id,
                command.FirstName,
                command.LastName,
                command.Email,
                command.Street ?? string.Empty,
                command.Number ?? string.Empty,
                command.City ?? string.Empty,
                command.PostalCode ?? string.Empty,
                command.Country ?? string.Empty,
                command.PhoneNumber,
                command.WebSite ?? string.Empty,
                command.Biography ?? string.Empty,
                command.Role);
        }
        catch (Exception e)
        {
            throw new Exception($"An error occurred while creating user: {e.Message}");
        }
    }
}