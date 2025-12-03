namespace Eventify.Services.IAM.Domain.Model.Commands;

/**
 * <summary>
 *     The sign up command
 * </summary>
 * <remarks>
 *     This command object includes user credentials and optional profile information
 * </remarks>
 */
public record SignUpCommand(
    string Email,
    string Password,
    string? Username = null,
    string? FirstName = null,
    string? LastName = null,
    string? PhoneNumber = null,
    string? Role = null,
    string? Street = null,
    string? Number = null,
    string? City = null,
    string? PostalCode = null,
    string? Country = null,
    string? WebSite = null,
    string? Biography = null);