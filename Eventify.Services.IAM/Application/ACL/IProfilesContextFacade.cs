namespace Eventify.Services.IAM.Application.ACL;

/**
 * <summary>
 *     Profiles context facade interface for IAM service
 * </summary>
 * <remarks>
 *     This facade allows IAM service to communicate with Profiles service
 * </remarks>
 */
public interface IProfilesContextFacade
{
    Task<int> CreateProfile(
        int userId,
        string firstName,
        string lastName,
        string email,
        string street,
        string number,
        string city,
        string postalCode,
        string country,
        string phoneNumber,
        string webSite,
        string biography,
        string role);
}
