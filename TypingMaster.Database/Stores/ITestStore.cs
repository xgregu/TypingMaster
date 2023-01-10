﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TypingMaster.Domain.Models;

namespace TypingMaster.Database.Stores;

public interface ITestStore
{
    Task<IReadOnlyList<Test>> GetAllTest();
    Task<TestTableDataResponse> GetTableData(int skipCount, int takeCount);
    Task Add(Test test);
    Task<int> GetTestRanking(Guid testId);
    Task<Test> FindLast();
    Task<Test> GetTest(Guid id);
}