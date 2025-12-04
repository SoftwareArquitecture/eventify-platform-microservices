namespace Eventify.Services.IAM.Interfaces.ACL;

public interface IIamContextFacade
{
    Task<int> CreateUser(
        string username,
        string password,
        string email,
        string firstName,
        string lastName,
        string phoneNumber,
        string street,
        string city,
        string country,
        string? role = null);

    Task<int> FetchUserIdByUsername(string username);
    Task<string> FetchUsernameByUserId(int userId);
}