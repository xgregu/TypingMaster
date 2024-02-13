namespace TypingMaster.Shared.Dtos;

public record CreateTestRequest(
    string ExecutorName,
    DateTimeOffset StartTime,
    DateTimeOffset EndTime,
    long TotalClicks,
    long TextId);