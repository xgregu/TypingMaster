using Blazorise.DataGrid;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TypingMaster.Database.Entities;
using TypingMaster.Domain.Models;

namespace TypingMaster.Database.Stores;

public class TestStore : ITestStore
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    public event EventHandler<Test> TestUpdated;

    public TestStore(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }


    public async Task<IReadOnlyList<Test>> GetAllTest()
    {
        await using var context =
            _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<TestDbContext>();
        return await GetQuerableTests(context, x => !string.IsNullOrWhiteSpace(x.Id.ToString())).ToListAsync();
    }

    public async Task<TestTableDataResponse> GetTableData(DataGridReadDataEventArgs<Test> dataArgs,
        Expression<Func<Test, bool>> predicate, DataGridColumnInfo sortColumnInfo)
    {
        await using var context =
            _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<TestDbContext>();

        var queryable = GetQuerableTests(context, predicate);
        var querableCount = queryable.Count();

        if (dataArgs.CancellationToken.IsCancellationRequested)
            return new TestTableDataResponse(queryable.ToList(), querableCount);

        var response = dataArgs.ReadDataMode switch
        {
            DataGridReadDataMode.Virtualize => queryable.Skip(dataArgs.VirtualizeOffset)
                .Take(dataArgs.VirtualizeCount),
            DataGridReadDataMode.Paging => queryable.Skip((dataArgs.Page - 1) * dataArgs.PageSize)
                .Take(dataArgs.PageSize),
            _ => throw new Exception("Unhandled ReadDataMode")
        };

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

    private static IQueryable<Test> GetQuerableTests(TestDbContext context,
        Expression<Func<Test, bool>> predicate)
    {
        var tests = context.Tests
            .Select(x => x.ToModel())
            .AsQueryable();

        return predicate == null ? tests : tests.Where(predicate);
    }
}