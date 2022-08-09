using System;
using TypingMaster.Domain;

namespace TypingMaster.Database.Entities;

public class TestEntity
{
    public int Id { get; set; }
    public Guid TestId { get; set; }
    public TypingTestType TestType { get; set; }
    public string Text { get; set; }
    public string ExecutorName { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public int CorrectClicks { get; set; }
    public int InorrectClicks { get; set; }
}