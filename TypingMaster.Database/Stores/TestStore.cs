using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TypingMaster.Database.Entities;
using TypingMaster.Domain.Models;

namespace TypingMaster.Database.Stores;

public class TestStore : ITestStore
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public TestStore(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task<IReadOnlyList<TestComplete>> GetAllTest()
    {
        await using var context =
            _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<TestDbContext>();

        return await GetEvents(context, x => !string.IsNullOrWhiteSpace(x.Id.ToString()));
    }

    public async Task Add(Test test)
    {
        await using var context =
            _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<TestDbContext>();
        var testEntity = test.ToEntity();
        await context.Tests.AddAsync(testEntity);
        await context.SaveChangesAsync();
    }

    private static async Task<IReadOnlyList<TestComplete>> GetEvents(TestDbContext context,
        Expression<Func<TestEntity, bool>> predicate)
    {
        return await context.Tests
            .Where(predicate)
            .Select(x => x.ToModel())
            .ToArrayAsync();
    }
}