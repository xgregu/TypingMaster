namespace TypingMaster.Domain.Models;

public class Test
{
    private Test(TypingTestType testType, string textToRewritten, bool isInitialized)
    {
        Id = Guid.NewGuid();
        TestType = testType;
        TextToRewritten = textToRewritten;
        IsInitialized = isInitialized;
    }

    public Guid Id { get; }
    public TypingTestType TestType { get; }
    public string TextToRewritten { get; }
    public string CurrentText { get; private set; }
    public bool IsInitialized { get; }
    public bool IsStarted => !string.IsNullOrWhiteSpace(CurrentText);
    public bool IsComplete { get; private set; }
    public int CompletionPercentage => IsStarted ? CurrentText.Length * 100 / TextToRewritten.Length : 0;
    public DateTime StartTime { get; private set; }
    public DateTime EndTime { get; private set; }
    public int CorrectClicks => TotalClicks - InorrectClicks;
    public int InorrectClicks { get; private set; }
    public int TotalClicks { get; private set; }

    public static Test EmptyTest()
    {
        return new Test(TypingTestType.Minimalistic, string.Empty, false);
    }

    public static Test InitializeTest(TypingTestType testType, string text)
    {
        return new Test(testType, text, true);
    }

    public void AppendNewChar(char newChar)
    {
        TotalClicks++;

        if (string.IsNullOrWhiteSpace(CurrentText))
            StartTime = DateTime.Now;

        var newText = CurrentText + newChar;

        if (!TextToRewritten.StartsWith(newText))
        {
            InorrectClicks++;
            return;
        }

        CurrentText = newText;

        if (TextToRewritten.Equals(CurrentText))
        {
            IsComplete = true;
            EndTime = DateTime.Now;
        }
    }
}