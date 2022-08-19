using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using TypingMaster.Database.Entities;
using TypingMaster.Domain;
using TypingMaster.Domain.Models;

namespace TypingMaster.Database.Stores;

public class TestStore : ITestStore
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ITestService _testService;
    public event EventHandler<Test> TestUpdated;

    public TestStore(IServiceScopeFactory serviceScopeFactory, ITestService testService)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _testService = testService;
    }


    public async Task<IReadOnlyList<Test>> GetAllTest()
    {
        await using var context =
            _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<TestDbContext>();
        var tests = context.Tests
            .Select(x => x.ToModel());
        return await tests.ToListAsync();
    }

    public async Task<TestTableDataResponse> GetTableData(int skipCount, int takeCount)
    {
        await using var context =
            _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<TestDbContext>();

        var queryable = context.Tests
            .OrderByDescending(x => x.TestDate)
            .Select(x => x.ToModel())
            .AsQueryable();

        var querableCount = queryable.Count();
        var response = queryable.Skip(skipCount).Take(takeCount);

        return new TestTableDataResponse(response.ToList(), querableCount);
    }

    public async Task Add(Test test)
    {
        await using var context =
            _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<TestDbContext>();
        var testEntity = test.ToEntity();
        await context.Tests.AddAsync(testEntity);
        await context.SaveChangesAsync();
        TestUpdated?.Invoke(this, test);
    }

    public async Task<int> GetTestRanking(Guid testId)
    {
        await using var context = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<TestDbContext>();
        var index = context.Tests
            .ToList()
            .OrderByDescending(x =>
            {
                var testStatistic = _testService.GetTestStatistic(x.ToModel());
                return testStatistic.OverallRating;

            }).ToList().FindIndex(x => x.TestId == testId);
        return index + 1;
    }

    public async Task<Test> FindLast()
    {
        await using var context = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<TestDbContext>();
        var entity = await context.Tests.AsNoTracking()
            .OrderByDescending(x => x.Id)
            .FirstOrDefaultAsync();

        return entity.ToModel();
    }
}