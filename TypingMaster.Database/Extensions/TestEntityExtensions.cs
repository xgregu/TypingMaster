using TypingMaster.Database.Entities;
using TypingMaster.Domain.Models;

namespace TypingMaster.Database.Extensions;

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
            TextToRewritten = model.TextToRewritten,
            ExecutorName = model.ExecutorName,
            TestDate = model.TestDate,
            InorrectClicks = model.InorrectClicks,
            TotalClicks = model.TotalClicks,
            StartTime = model.StartTime,
            EndTime = model.EndTime
        };
    }

    public static Test ToModel(this TestEntity entity)
    {
        if (entity is null)
            return null;

        return new Test
        {
            Id = entity.TestId,
            TestType = entity.TestType,
            TextToRewritten = entity.TextToRewritten,
            ExecutorName = entity.ExecutorName,
            TestDate = entity.TestDate,
            InorrectClicks = entity.InorrectClicks,
            TotalClicks = entity.TotalClicks,
            EndTime = entity.EndTime,
            StartTime = entity.StartTime
        };
    }
}