using Blazorise.DataGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TypingMaster.Domain.Models;

namespace TypingMaster.Database.Stores;

public interface ITestStore
{
    public event EventHandler<Test> TestUpdated;

    Task<IReadOnlyList<Test>> GetAllTest();
    Task<TestTableDataResponse> GetTableData(int skipCount, int takeCount);
    Task Add(Test test);
}