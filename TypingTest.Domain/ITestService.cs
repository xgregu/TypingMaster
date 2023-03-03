using TypingMaster.Domain.Models;

namespace TypingMaster.Domain;

public interface ITestService
{
    TestStatistic GetTestStatistic(Test test);
}