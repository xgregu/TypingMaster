using TypingMaster.Domain.Models;

namespace TypingMaster.Domain.Options;

public class TypingTestOptions
{
    public const string SectionKey = "TypingTest";
    public TypingTestTextOPTIONS[] TypingTestTexts { get; set; }
}