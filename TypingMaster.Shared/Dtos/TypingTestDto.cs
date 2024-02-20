namespace TypingMaster.Shared.Dtos;

public record TypingTestDto(
    long Id,
    string ExecutorName,
    DateTimeOffset StartTime,
    DateTimeOffset EndTime,
    TypingTextDto Text,
    TypingTestStatisticsDto Statistics);