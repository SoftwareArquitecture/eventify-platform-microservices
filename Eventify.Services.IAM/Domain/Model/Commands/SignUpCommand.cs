namespace Eventify.Services.IAM.Domain.Model.Commands;

/**
 * <summary>
 *     The sign up command
 * </summary>
 * <remarks>
 *     This command object includes user credentials and required profile information
 * </remarks>
 */
public record SignUpCommand(
    string Username,
    string Password,
    string Email,
    string FirstName,
    string LastName,
    string PhoneNumber,
    string Street,
    string City,
    string Country,
    string? Role = null);