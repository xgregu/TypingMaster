using System.Text.Json;
using MediatR;
using TypingMaster.Application;
using TypingMaster.Application.Functions.Tests.Commands.CreateTest;
using TypingMaster.Application.Functions.TypingTexts.Queries.GetTypingTextsByDifficultyLevel;
using TypingMaster.Application.Interfaces;
using TypingMaster.Domain;
using TypingMaster.Domain.Entities;
using TypingMaster.Shared.Dtos;

namespace TypingMaster.DevTools;

public class RandomTestProvider(ICulturesStore culturesStore, ITypingTextsStore typingTextsStore,
    ITypingTestStore typingTestStore, ITestStatisticsCalculator statisticsCalculator) : IInitializable
{
    public uint Priority => uint.MaxValue;

    public async Task Initialize()
    {
        var cultures = await culturesStore.GetAllAsync();

        for (int i = 0; i < 1000; i++)
        {
            _ = Task.Run(async () =>
            {
                while (true)
                {
                    try
                    {
                        using var httpClient = new HttpClient();

                        var response = await httpClient.GetAsync("https://api.namefake.com/");
                        if (!response.IsSuccessStatusCode)
                            continue;

                        var responseContent = await response.Content.ReadAsStringAsync();
                        var fakePerson = JsonSerializer.Deserialize<FakePerson>(responseContent)!;

                        var startDate = DateTimeOffset.Now;
                        var endDate = DateTimeOffset.Now.AddSeconds(new Random().Next(5, 240));

                        var level = (uint) new Random().Next(1, 5);
                        var culture = cultures.OrderBy(x => Guid.NewGuid()).First();

                        var texts = await typingTextsStore.GetByDifficultyLevelAsync(level, culture.CultureCode);
                        var text = texts.OrderBy(x => Guid.NewGuid()).First();
                        var textLenght = text.Text.Length;
                        var maxTotalCLicks = textLenght + (textLenght * 25 / 100);
                        var totalClicks = new Random().Next(textLenght, maxTotalCLicks);

                        var test = new CreateTestRequest(fakePerson.name, startDate, endDate, totalClicks, text.Id);
                        var request = new CreatedTestCommand(test);
                        var testStatistisc = await statisticsCalculator.GetTestStatistic(request.CreateTestRequest);
                        var testEntity = new TypingTestEntity
                        {
                            ExecutorName = request.CreateTestRequest.ExecutorName,
                            StartTime = request.CreateTestRequest.StartTime,
                            EndTime = request.CreateTestRequest.EndTime,
                            TextId = request.CreateTestRequest.TextId,
                            Statistics = testStatistisc
                        };

                        _ = typingTestStore.AddAsync(testEntity);
                    }
                    catch
                        (Exception e)
                    {
                    }
                }
            });
        }
    }
}

public class FakePerson
{
    public string name { get; set; }
    public string address { get; set; }
    public double latitude { get; set; }
    public double longitude { get; set; }
    public string maiden_name { get; set; }
    public string birth_data { get; set; }
    public string phone_h { get; set; }
    public string phone_w { get; set; }
    public string email_u { get; set; }
    public string email_d { get; set; }
    public string username { get; set; }
    public string password { get; set; }
    public string domain { get; set; }
    public string useragent { get; set; }
    public string ipv4 { get; set; }
    public string macaddress { get; set; }
    public string plasticcard { get; set; }
    public string cardexpir { get; set; }
    public int bonus { get; set; }
    public string company { get; set; }
    public string color { get; set; }
    public string uuid { get; set; }
    public int height { get; set; }
    public double weight { get; set; }
    public string blood { get; set; }
    public string eye { get; set; }
    public string hair { get; set; }
    public string pict { get; set; }
    public string url { get; set; }
    public string sport { get; set; }
    public string ipv4_url { get; set; }
    public string email_url { get; set; }
}