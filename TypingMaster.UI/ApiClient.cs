using System.Text.Json;
using Microsoft.Extensions.Caching.Memory;
using TypingMaster.Shared.Dtos;

namespace TypingMaster.UI;

public class ApiClient(IHttpClientFactory httpClientFactory, IMemoryCache memoryCache)
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient("ApiClient");

    private const string TestApiUrl = "Test";
    private static string GetTestUrl(long testId) => $"{TestApiUrl}/{testId}";
    private static string GetTestRankingUrl(long testId) => $"{TestApiUrl}/{testId}/ranking";
    private static string GetTestPageUrl(long startIndex, long count) => $"{TestApiUrl}/paged?startIndex={startIndex}&count={count}";
    private static string GetTestCountUrl() => $"{TestApiUrl}/count";

    private const string TypingLevelApiUrl = "TypingLevel";
    private const string TypingTextApiUrl = "TypingText";
    private static string GetTypingTextByDifficultyLevelUrl(uint difficultyLevel) => $"{TypingTextApiUrl}/{difficultyLevel}";

    public async Task<TypingTestDto?> GetTest(long testId)
    {
        var cacheKey = $"GetTest-{testId}";
        if (memoryCache.TryGetValue(cacheKey, out TypingTestDto? cachedData))
            return cachedData;

        var response = await _httpClient.GetAsync(GetTestUrl(testId));
        var typingTestDto = await HandleResponse<TypingTestDto>(response);
        memoryCache.Set(cacheKey, typingTestDto, TimeSpan.FromMinutes(1));
        return typingTestDto;
    }

    public async Task<long> GetTestRanking(long testId)
    {
        var response = await _httpClient.GetAsync(GetTestRankingUrl(testId));
        var ranking = await HandleResponse<long>(response);
        return ranking;
    }
    
    public async Task<long> GetTestCount()
    {
        var response = await _httpClient.GetAsync(GetTestCountUrl());
        var ranking = await HandleResponse<long>(response);
        return ranking;
    }

    public async Task<ICollection<TypingTestDto>?> GetAllTests()
    {
        var response = await _httpClient.GetAsync(TestApiUrl);
        var typingTestDto = await HandleResponse<TypingTestDto[]>(response);
        return typingTestDto;
    }
    
    public async Task<ICollection<TypingTestDto>?> GetTestPage(long startIndex, long count)
    {
        var response = await _httpClient.GetAsync(GetTestPageUrl(startIndex, count));
        var typingTestDto = await HandleResponse<TypingTestDto[]>(response);
        return typingTestDto;
    }

    public async Task<ICollection<TypingTextDto>?> GetAllTypingTypingTextByDifficultyLevel(uint difficultyLevel)
    {
        var cacheKey = $"TypingTextByDifficulty-{difficultyLevel}";
        if (memoryCache.TryGetValue(cacheKey, out ICollection<TypingTextDto>? cachedData))
            return cachedData;

        var response = await _httpClient.GetAsync(GetTypingTextByDifficultyLevelUrl(difficultyLevel));
        var typingTextDtos = await HandleResponse<TypingTextDto[]>(response);
        memoryCache.Set(cacheKey, typingTextDtos, TimeSpan.FromMinutes(1));
        return typingTextDtos;
    }

    public async Task<ICollection<TypingLevelDto>?> GetAllTypingLevels()
    {
        const string cacheKey = "AllTypingLevels";
        if (memoryCache.TryGetValue(cacheKey, out ICollection<TypingLevelDto>? cachedData))
            return cachedData;

        var response = await _httpClient.GetAsync(TypingLevelApiUrl);
        var typingLevelDtos = await HandleResponse<TypingLevelDto[]>(response);
        memoryCache.Set(cacheKey, typingLevelDtos, TimeSpan.FromMinutes(1));
        return typingLevelDtos;
    }

    public async Task<TypingTestDto?> CreateTest(CreateTestRequest createTestRequest)
    {
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