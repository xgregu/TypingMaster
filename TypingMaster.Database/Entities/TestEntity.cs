﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TypingMaster.Domain;
using TypingMaster.Domain.Models;

namespace TypingMaster.Database.Entities;

public class TestEntity
{
    public int Id { get; set; }
    public Guid TestId { get; set; }
    public TypingTestType TestType { get; set; }
    public string TextToRewritten { get; set; }
    public string ExecutorName { get; set; }
    public DateTime TestDate { get; set; }
    public int InorrectClicks { get; set; }
    public int TotalClicks { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}

internal class TestEntityConfiguration : IEntityTypeConfiguration<TestEntity>
{
    public void Configure(EntityTypeBuilder<TestEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TestId).IsRequired();
        builder.Property(x => x.TestType).IsRequired();
        builder.Property(x => x.TextToRewritten).IsRequired();
        builder.Property(x => x.ExecutorName).IsRequired();
        builder.Property(x => x.InorrectClicks).IsRequired();
        builder.Property(x => x.TotalClicks).IsRequired();
        builder.Property(x => x.StartTime).IsRequired();
    }
}

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