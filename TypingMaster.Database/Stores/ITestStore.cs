using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Blazorise.DataGrid;
using TypingMaster.Domain.Models;

namespace TypingMaster.Database.Stores;

public interface ITestStore
{
    public event EventHandler<Test> TestUpdated;

    Task<IReadOnlyList<Test>> GetAllTest();
    Task<TestTableDataResponse> GetTableData(DataGridReadDataEventArgs<Test> dataArgs, Expression<Func<Test, bool>> expression, DataGridColumnInfo sortColumnInfo);
    Task Add(Test test);
}