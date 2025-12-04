namespace Eventify.Services.IAM.Domain.Model.Commands;

/**
 * <summary>
 *     The delete user command
 * </summary>
 * <remarks>
 *     This command object includes the user id to delete
 * </remarks>
 */
public record DeleteUserCommand(int UserId);
