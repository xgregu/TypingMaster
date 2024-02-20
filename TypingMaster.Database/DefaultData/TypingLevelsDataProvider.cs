using TypingMaster.Database.DefaultData.Interface;
using TypingMaster.Domain.Entities;

namespace TypingMaster.Database.DefaultData;

public class TypingLevelsDataProvider
{
    public IEnumerable<TypingLevelEntity> TypingLevels { get; } = GetTypingLevels();
    private static IEnumerable<TypingLevelEntity> GetTypingLevels() =>
        new List<TypingLevelEntity>
        {
            new() {DifficultyLevel = 1, Name = "Minimalist", DifficultyCoefficient = 0.6},
            new() {DifficultyLevel = 2, Name = "Short", DifficultyCoefficient = 0.8},
            new() {DifficultyLevel = 3, Name = "Standard", DifficultyCoefficient = 1.0},
            new() {DifficultyLevel = 4, Name = "Long", DifficultyCoefficient = 1.2},
            new() {DifficultyLevel = 5, Name = "Very Long", DifficultyCoefficient = 1.4},
        };
}