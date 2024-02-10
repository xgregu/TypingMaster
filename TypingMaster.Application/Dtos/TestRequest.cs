namespace TypingMaster.Application.Dtos;

public record TestRequest(
    string ExecutorName,
    DateTimeOffset StartTime,
    DateTimeOffset EndTime,
    long TotalClicks,
    long TextId);