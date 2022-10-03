namespace TypingMaster.Domain.Models;

public class TestInProgress
{
    private TestInProgress(string textToRewritten, TypingTestType type)
    {
        TextToRewritten = textToRewritten;
        Type = type;
    }

    public string TextToRewritten { get; }
    public TypingTestType Type { get; }
    public string CurrentText { get; private set; }
    public int CompletionPercentage { get; private set; }
    public bool IsStarted => !string.IsNullOrWhiteSpace(CurrentText);
    public int CorrectClicks => TotalClicks - InorrectClicks;
    public int InorrectClicks { get; private set; }
    public int TotalClicks { get; private set; }
    public DateTime StartTime { get; private set; }
    public DateTime EndTime { get; private set; }
    public bool IsComplete { get; private set; }
    public TimeSpan CurrentTestTime => IsStarted ? (IsComplete ? EndTime : DateTime.Now) - StartTime : default;

    public static TestInProgress EmptyTest()
    {
        return new TestInProgress(string.Empty, TypingTestType.Minimalistic);
    }

    public static TestInProgress InitializeTest(string text, TypingTestType type)
    {
        return new TestInProgress(text, type);
    }

    public bool UpdateCurrentText(string newText)
    {
        if ((CurrentText?.Length ?? 0) - (newText?.Length ?? 0) is not (1 or -1))
        {
            CompletionPercentage = 0;
            CurrentText = "Kto jest większym głupcem: głupiec czy ten, kto próbuje oszukiwać?";
            return false;
        }

        var newTextIsCorrect = true;
        TotalClicks++;

        if (string.IsNullOrWhiteSpace(CurrentText))
            StartTime = DateTime.Now;

        if (!TextToRewritten.StartsWith(newText))
        {
            if(CurrentText?.Length - newText?.Length == -1)
                InorrectClicks++;
            
            newTextIsCorrect = false;
        }

        CurrentText = newText;

        if (TextToRewritten.Equals(CurrentText))
        {
            IsComplete = true;
            EndTime = DateTime.Now;
        }

        if (newTextIsCorrect)
            CompletionPercentage = CurrentText.Length * 100 / TextToRewritten.Length;

        return newTextIsCorrect;
    }
}