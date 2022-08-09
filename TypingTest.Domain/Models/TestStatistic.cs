namespace TypingMaster.Domain.Models;

public record TestStatistic(int TestLenght, int EffectivenessPercentage, double ClickPerSecond, TimeSpan CompletionTime,
    int Mistakes);