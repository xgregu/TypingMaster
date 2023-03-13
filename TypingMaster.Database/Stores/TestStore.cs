using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TypingMaster.Database.Extensions;
using TypingMaster.Domain;
using TypingMaster.Domain.Events;
using TypingMaster.Domain.Models;

namespace TypingMaster.Database.Stores;

public class TestStore : ITestStore
{
    private readonly IMediator _mediator;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ITestService _testService;

    public TestStore(IServiceScopeFactory serviceScopeFactory, ITestService testService, IMediator mediator)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _testService = testService;
        _mediator = mediator;
    }

    public async Task<IReadOnlyList<Test>> GetAllTest()
    {
        await using var context =
            _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<TestDbContext>();
        var tests = context.Tests
            .AsNoTracking()
            .Select(x => x.ToModel());
        return await tests.ToListAsync();
    }

    public async Task<TestTableDataResponse> GetTableData(int skipCount, int takeCount)
    {
        await using var context =
            _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<TestDbContext>();

        var queryable = context.Tests
            .AsNoTracking()
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
        await _mediator.Publish(new TestUpdated(test));
    }

    public async Task<int> GetTestRanking(Guid testId)
    {
        await using var context =
            _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<TestDbContext>();
        var index = context.Tests
            .AsNoTracking()
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
        await using var context =
            _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<TestDbContext>();
        var entity = await context.Tests.AsNoTracking()
            .OrderByDescending(x => x.Id)
            .FirstOrDefaultAsync();

        return entity.ToModel();
    }

    public async Task<Test> GetTest(Guid id)
    {
        await using var context =
            _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<TestDbContext>();

        var entity = await context.Tests.AsNoTracking()
            .FirstOrDefaultAsync(x => x.TestId == id);

        return entity.ToModel();
    }
}