namespace Eventify.Services.IAM.Domain.Model.Commands;

/**
 * <summary>
 *     The sign up command
 * </summary>
 * <remarks>
 *     This command object includes user credentials and profile information
 * </remarks>
 */
public record SignUpCommand(
    string Username,
    string Password,
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    string Role,
    string? Street,
    string? Number,
    string? City,
    string? PostalCode,
    string? Country,
    string? WebSite,
    string? Biography);