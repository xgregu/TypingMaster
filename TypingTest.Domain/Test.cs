namespace TypingTest.Domain;

public class Test
{
    private Test(TypingTestType testType, string textToRewritten, bool isInitialized)
    {
        TestType = testType;
        TextToRewritten = textToRewritten;
        IsInitialized = isInitialized;
    }

    public TypingTestType TestType { get; }
    public string TextToRewritten { get; }
    public string CurrentText { get; private set; }
    public bool IsInitialized { get; }
    public bool IsStarted => !string.IsNullOrWhiteSpace(CurrentText);
    public bool IsComplete { get; private set; }
    public int CompletionPercentage => IsStarted ? CurrentText.Length * 100 / TextToRewritten.Length : 0;
    public int EffectivenessPercentage { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; private set; }
    public TimeSpan CompletionTime => EndTime - StartTime;
    public int Clicks { get; set; }

    public static Test EmptyTest()
    {
        return new Test(TypingTestType.Minimalistic, string.Empty, false);
    }

    public static Test InitializeTest(TypingTestType testType, string text)
    {
        return new Test(testType, text, true);
    }

    public bool AppendNewChar(char newChar)
    {
        Clicks++;

        if(string.IsNullOrWhiteSpace(CurrentText))
            StartTime = DateTime.Now;

        var newText = CurrentText + newChar;

        if (!TextToRewritten.StartsWith(newText))
            return false;

        CurrentText = newText;

        if (TextToRewritten.Equals(CurrentText))
        {
            IsComplete = true;
            EndTime = DateTime.Now;
        }

        return true;
    }
}