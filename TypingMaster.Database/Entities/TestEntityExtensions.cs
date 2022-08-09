using TypingMaster.Domain.Models;

namespace TypingMaster.Database.Entities;

internal static class TestEntityExtensions
{
    public static TestEntity ToEntity(this Test model)
    {
        if (model is null)
            return null;


        return new TestEntity
        {
            TestId = model.Id,
            TestType = model.TestType,
            Text = model.TextToRewritten,
            ExecutorName = model.ExecutorName,
            StartTime = model.StartTime,
            EndTime = model.EndTime,
            CorrectClicks = model.CorrectClicks,
            InorrectClicks = model.InorrectClicks
        };
    }

    public static TestComplete ToModel(this TestEntity entity)
    {
        if (entity is null) return null;

        return new TestComplete
        {
            Id = entity.TestId,
            Text = entity.Text,
            ExecutorName = entity.ExecutorName,
            TestType = entity.TestType,
            StartTime = entity.StartTime,
            EndTime = entity.EndTime,
            CorrectClicks = entity.CorrectClicks,
            InorrectClicks = entity.InorrectClicks
        };
    }
}