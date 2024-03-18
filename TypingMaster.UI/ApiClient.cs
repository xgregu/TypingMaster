using System.Text.Json;
using TypingMaster.Shared.Dtos;
using TypingMaster.UI.Localizations;

namespace TypingMaster.UI;

public class ApiClient(ILogger<ApiClient> logger, IHttpClientFactory httpClientFactory, ICultureContext cultureContext,
    SignalRConnectivity signalRConnectivity)
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient(ApiClientName);
    private const string ApiClientName = "ApiClient";
    private const string TestApiUrl = "TypingTest";
    private const string TypingLevelApiUrl = "TypingLevel";
    private const string TypingTextApiUrl = "TypingText";
    
    private string CurrentCultureName => cultureContext.CurrentCulture.Name;
    private string GetAllTypingLevelsUrl => $"{TypingLevelApiUrl}?cultureCode={CurrentCultureName}";
    private bool IsConnected => signalRConnectivity.IsConnected;
    private static string GetTestUrl(long testId) => $"{TestApiUrl}/{testId}";
    private static string GetTestRankingUrl(long testId) => $"{TestApiUrl}/{testId}/ranking";

    private static string GetTestPageUrl(long startIndex, long count) =>
        $"{TestApiUrl}/paged?startIndex={startIndex}&count={count}";

    private string GetTypingLevelNameUrl(uint difficultyLevel) =>
        $"{TypingLevelApiUrl}/{difficultyLevel}?cultureCode={CurrentCultureName}";

    private string GetTypingTextByDifficultyLevelUrl(uint difficultyLevel) =>
        $"{TypingTextApiUrl}/{difficultyLevel}?cultureCode={CurrentCultureName}";

    public async Task<TypingTestDto?> GetTest(long testId, CancellationToken cancellationToken = default) =>
        await PerformRequest<TypingTestDto>(() =>
            _httpClient.GetAsync(GetTestUrl(testId), cancellationToken), cancellationToken);

    public async Task<long?> GetTestRanking(long testId, CancellationToken cancellationToken = default) =>
        await PerformRequest<long?>(() =>
            _httpClient.GetAsync(GetTestRankingUrl(testId), cancellationToken), cancellationToken);

    public async Task<ICollection<TypingTestDto>?> GetAllTests(CancellationToken cancellationToken = default) =>
        await PerformRequest<ICollection<TypingTestDto>?>(() =>
            _httpClient.GetAsync(TestApiUrl, cancellationToken), cancellationToken);

    public async Task<PagedTestResponse?> GetTestPage(long startIndex, long count,
        CancellationToken cancellationToken = default) =>
        await PerformRequest<PagedTestResponse?>(() =>
            _httpClient.GetAsync(GetTestPageUrl(startIndex, count), cancellationToken), cancellationToken);
    
    public async Task<ICollection<TypingTextDto>?> GetAllTypingTypingTextByDifficultyLevel(uint difficultyLevel,
        CancellationToken cancellationToken = default) =>
        await PerformRequest<ICollection<TypingTextDto>?>(() =>
            _httpClient.GetAsync(GetTypingTextByDifficultyLevelUrl(difficultyLevel), cancellationToken), cancellationToken);

    public async Task<ICollection<TypingLevelDto>?> GetAllTypingLevels(
        CancellationToken cancellationToken = default) =>
        await PerformRequest<ICollection<TypingLevelDto>?>(() =>
            _httpClient.GetAsync(GetAllTypingLevelsUrl, cancellationToken), cancellationToken);
    
    public async Task<string?> GetTypingLevelName(uint difficultyLevel, 
        CancellationToken cancellationToken = default) =>
        await PerformRequest<string?>(() =>
            _httpClient.GetAsync(GetTypingLevelNameUrl(difficultyLevel), cancellationToken), cancellationToken);
    
    public async Task<TypingTestDto?> CreateTest(CreateTestRequest createTestRequest, 
        CancellationToken cancellationToken = default) =>
        await PerformRequest<TypingTestDto?>(() =>
            _httpClient.PostAsJsonAsync(TestApiUrl, createTestRequest, cancellationToken: cancellationToken), cancellationToken);
    
    private async Task<T?> PerformRequest<T>(Func<Task<HttpResponseMessage>> requestFunc, CancellationToken cancellationToken = default)
    {
        if (!IsConnected)
        {
            logger.LogWarning("Backend is not available");
            return default;
        }

        if (cancellationToken.IsCancellationRequested)
        {
            logger.LogWarning("Request is canceled");
            return default;
        }

        try
        {
            var response = await requestFunc.Invoke();
            return await HandleResponse<T>(response);
        }
        catch (OperationCanceledException)
        {
            logger.LogWarning("Request canceled");
            return default;
        }
        catch (Exception e)
        {
            logger.LogWarning(e, "Request canceled");
            return default;
        }
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