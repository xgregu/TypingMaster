using TypingMaster.Domain.Models;

namespace TypingMaster.Domain.Options;

public class TypingTestOptions
{
    public const string SectionKey = "TypingTest";
    public TypingTestTextOptions[] TypingTestTexts { get; set; }
}