using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace Eventify.Services.IAM.Application.ACL;

/**
 * <summary>
 *     Profiles context facade implementation
 * </summary>
 * <remarks>
 *     This facade communicates with the Profiles microservice via HTTP
 * </remarks>
 */
public class ProfilesContextFacade : IProfilesContextFacade
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public ProfilesContextFacade(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task<int> CreateProfile(
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
        string role)
    {
        var profilesServiceUrl = _configuration["ProfilesServiceUrl"] ?? "http://profiles-service:8080";

        var createProfileRequest = new
        {
            userId,
            firstName,
            lastName,
            email,
            street,
            number,
            city,
            postalCode,
            country,
            phoneNumber,
            webSite,
            biography,
            role
        };

        var json = JsonSerializer.Serialize(createProfileRequest);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync($"{profilesServiceUrl}/api/v1/profiles", content);

        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            throw new Exception($"Failed to create profile: {response.StatusCode} - {errorContent}");
        }

        var responseContent = await response.Content.ReadAsStringAsync();
        var jsonDocument = JsonDocument.Parse(responseContent);

        // Extract the profile ID from the response
        if (jsonDocument.RootElement.TryGetProperty("id", out var idElement))
        {
            return idElement.GetInt32();
        }

        throw new Exception("Failed to extract profile ID from response");
    }
}
