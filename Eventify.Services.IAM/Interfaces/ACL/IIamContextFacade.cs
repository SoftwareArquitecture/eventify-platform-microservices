namespace Eventify.Services.IAM.Interfaces.ACL;

public interface IIamContextFacade
{
    Task<int> CreateUser(
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
        string? biography = null);

    Task<int> FetchUserIdByUsername(string username);
    Task<string> FetchUsernameByUserId(int userId);
}