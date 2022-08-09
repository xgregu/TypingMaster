using System.Collections.Generic;
using System.Threading.Tasks;
using TypingMaster.Domain.Models;

namespace TypingMaster.Database.Stores;

public interface ITestStore
{
    Task<IReadOnlyList<TestComplete>> GetAllTest();
    Task Add(Test test);
}