using PSC.Blazor.Components.BrowserDetect;
using TypingMaster.Domain.Models;

namespace TypingMaster.Domain;

public interface ITestService
{
    Test TestInProgressEnd(TestInProgress testInProgress, string executorName);
    TestStatistic GetTestStatistic(Test test);
}