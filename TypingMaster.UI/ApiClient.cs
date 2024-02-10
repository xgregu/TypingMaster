using System.Text.Json;
using TypingMaster.UI.Dtos;

namespace TypingMaster.UI;

public class ApiClient
{
    private readonly HttpClient _httpClient;
    private const string TestApiUrl = "Test";
    
    public ApiClient(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("ApiClient");
        _ = CreateTest(new TestRequest("test", DateTimeOffset.Now.AddMinutes(-1), DateTimeOffset.Now, 21453, 1));
    }

    public async Task<TypingTestDto?> GetTest(long testId)
    {
        var response = await _httpClient.GetAsync($"{TestApiUrl}/{testId}");
        var typingTestDto = await HandleResponse<TypingTestDto>(response);
        return typingTestDto;
    }

    public async Task<ICollection<TypingTestDto>?> GetAllTests()
    {
        var response = await _httpClient.GetAsync(TestApiUrl);
        var typingTestDto = await HandleResponse<TypingTestDto[]>(response);
        return typingTestDto;
    }

    public async Task<TypingTestDto?> CreateTest(TestRequest testRequest)
    {
        var response = await _httpClient.PostAsJsonAsync(TestApiUrl, testRequest);
        var typingTestDto = await HandleResponse<TypingTestDto>(response);
        return typingTestDto;
    }
    
    private static async Task<TDto?> HandleResponse<TDto>(HttpResponseMessage response)
    {
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();

        using var doc = JsonDocument.Parse(responseContent);
        var jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true};

        return doc.RootElement.TryGetProperty("item", out var itemElement) 
            ? JsonSerializer.Deserialize<TDto>(itemElement.GetRawText(), jsonOptions) 
            : default;
    }
    
}