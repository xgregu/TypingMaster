using System.Text.Json;
using TypingMaster.Shared.Dtos;
using TypingMaster.UI.Localizations;

namespace TypingMaster.UI;

public class ApiClient(ILogger<ApiClient> logger, IHttpClientFactory httpClientFactory, ICultureContext cultureContext, SignalRConnectivity signalRConnectivity)
{
    private const string TestApiUrl = "TypingTest";

    private const string TypingLevelApiUrl = "TypingLevel";

    private const string TypingTextApiUrl = "TypingText";
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient("ApiClient");
    private string CurrentCultureName => cultureContext.CurrentCulture.Name;
    private string GetAllTypingLevelsUrl => $"{TypingLevelApiUrl}?cultureCode={CurrentCultureName}";
    private bool IsConnected => signalRConnectivity.IsConnected;
    
    private static string GetTestUrl(long testId)
    {
        return $"{TestApiUrl}/{testId}";
    }

    private static string GetTestRankingUrl(long testId)
    {
        return $"{TestApiUrl}/{testId}/ranking";
    }

    private static string GetTestPageUrl(long startIndex, long count)
    {
        return $"{TestApiUrl}/paged?startIndex={startIndex}&count={count}";
    }

    private string GetTypingLevelNameUrl(uint difficultyLevel)
    {
        return $"{TypingLevelApiUrl}/{difficultyLevel}?cultureCode={CurrentCultureName}";
    }

    private string GetTypingTextByDifficultyLevelUrl(uint difficultyLevel)
    {
        return $"{TypingTextApiUrl}/{difficultyLevel}?cultureCode={CurrentCultureName}";
    }

    public async Task<TypingTestDto?> GetTest(long testId)
    {
        if (!IsConnected)
        {
            logger.LogWarning("Backend is not avaiable");
            return null;
        }
        
        var response = await _httpClient.GetAsync(GetTestUrl(testId));
        var typingTestDto = await HandleResponse<TypingTestDto>(response);
        return typingTestDto;
    }

    public async Task<long?> GetTestRanking(long testId, CancellationToken cancellationToken = default)
    {
        if (!IsConnected)
        {
            logger.LogWarning("Backend is not avaiable");
            return null;
        }
        
        try
        {
            var response = await _httpClient.GetAsync(GetTestRankingUrl(testId), cancellationToken);
            var ranking = await HandleResponse<long>(response);
            return ranking;
        }
        catch (OperationCanceledException)
        {
            return null;
        }
    }

    public async Task<ICollection<TypingTestDto>?> GetAllTests()
    {
        if (!IsConnected)
        {
            logger.LogWarning("Backend is not avaiable");
            return null;
        }
        
        var response = await _httpClient.GetAsync(TestApiUrl);
        var typingTestDto = await HandleResponse<TypingTestDto[]>(response);
        return typingTestDto;
    }

    public async Task<PagedTestResponse?> GetTestPage(long startIndex, long count,
        CancellationToken cancellationToken = default)
    {
        if (!IsConnected)
        {
            logger.LogWarning("Backend is not avaiable");
            return null;
        }
        
        try
        {
            var response = await _httpClient.GetAsync(GetTestPageUrl(startIndex, count), cancellationToken);
            var typingTestDto = await HandleResponse<PagedTestResponse>(response);
            return typingTestDto;
        }
        catch (OperationCanceledException)
        {
            return null;
        }
    }

    public async Task<ICollection<TypingTextDto>?> GetAllTypingTypingTextByDifficultyLevel(uint difficultyLevel)
    {
        if (!IsConnected)
        {
            logger.LogWarning("Backend is not avaiable");
            return null;
        }
        
        var response = await _httpClient.GetAsync(GetTypingTextByDifficultyLevelUrl(difficultyLevel));
        var typingTextDtos = await HandleResponse<TypingTextDto[]>(response);
        return typingTextDtos;
    }

    public async Task<ICollection<TypingLevelDto>?> GetAllTypingLevels()
    {
        if (!IsConnected)
        {
            logger.LogWarning("Backend is not avaiable");
            return null;
        }
        
        var response = await _httpClient.GetAsync(GetAllTypingLevelsUrl);
        var typingLevelDtos = await HandleResponse<TypingLevelDto[]>(response);
        return typingLevelDtos;
    }

    public async Task<string?> GetTypingLevelName(uint difficultyLevel, CancellationToken cancellationToken = default)
    {
        if (!IsConnected)
        {
            logger.LogWarning("Backend is not avaiable");
            return null;
        }
        
        try
        {
            var response = await _httpClient.GetAsync(GetTypingLevelNameUrl(difficultyLevel), cancellationToken);
            var levelName = await HandleResponse<string>(response);
            return levelName;
        }
        catch (OperationCanceledException)
        {
            return null;
        }
    }

    public async Task<TypingTestDto?> CreateTest(CreateTestRequest createTestRequest)
    {
        if (!IsConnected)
        {
            logger.LogWarning("Backend is not avaiable");
            return null;
        }
        
        var response = await _httpClient.PostAsJsonAsync(TestApiUrl, createTestRequest);
        var typingTestDto = await HandleResponse<TypingTestDto>(response);
        return typingTestDto;
    }

    private static async Task<TDto?> HandleResponse<TDto>(HttpResponseMessage response)
    {
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();

        using var doc = JsonDocument.Parse(responseContent);
        var jsonOptions = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};

        return doc.RootElement.TryGetProperty("item", out var itemElement)
            ? JsonSerializer.Deserialize<TDto>(itemElement.GetRawText(), jsonOptions)
            : default;
    }
}