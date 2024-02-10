namespace TypingMaster.UI.Dtos;

public record TestRequest(
    string ExecutorName,
    DateTimeOffset StartTime,
    DateTimeOffset EndTime,
    long TotalClicks,
    long TextId);