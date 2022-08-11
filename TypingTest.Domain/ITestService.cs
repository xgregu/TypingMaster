using TypingMaster.Domain.Models;

namespace TypingMaster.Domain;

public interface ITestService
{
    Test TestInProgressEnd(TestInProgress testInProgress);
}