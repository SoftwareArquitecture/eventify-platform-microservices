using Eventify.Services.IAM.Domain.Model.Aggregates;
using Eventify.Services.IAM.Domain.Model.Commands;

namespace Eventify.Services.IAM.Domain.Services;

/**
 * <summary>
 *     The user command service
 * </summary>
 * <remarks>
 *     This interface is used to handle user commands
 * </remarks>
 */
public interface IUserCommandService
{
    /**
     * <summary>
     *     Handle sign in command
     * </summary>
     * <param name="command">The sign in command</param>
     * <returns>The authenticated user and the JWT token</returns>
     */
    Task<(User user, string token)> Handle(SignInCommand command);

    /**
     * <summary>
     *     Handle sign up command
     * </summary>
     * <param name="command">The sign up command</param>
     * <returns>A confirmation message on successful creation.</returns>
     */
    Task Handle(SignUpCommand command);

    /**
     * <summary>
     *     Handle delete user command
     * </summary>
     * <param name="command">The delete user command</param>
     * <returns>A task representing the asynchronous operation</returns>
     */
    Task Handle(DeleteUserCommand command);
}