namespace TypingMaster.Domain.Models;

public class Test
{
    private Test(TypingTestType testType, string textToRewritten, bool isInitialized, string executorName)
    {
        Id = Guid.NewGuid();
        TestType = testType;
        TextToRewritten = textToRewritten;
        IsInitialized = isInitialized;
        ExecutorName = executorName;
    }

    public Guid Id { get; }
    public TypingTestType TestType { get; }
    public string TextToRewritten { get; }
    public string ExecutorName { get; }
    public string CurrentText { get; private set; }
    public bool IsInitialized { get; }
    public bool IsStarted => !string.IsNullOrWhiteSpace(CurrentText);
    public bool IsComplete { get; private set; }
    public int CompletionPercentage { get; private set; }
    public DateTime StartTime { get; private set; }
    public DateTime EndTime { get; private set; }
    public int CorrectClicks => TotalClicks - InorrectClicks;
    public int InorrectClicks { get; private set; }
    public int TotalClicks { get; private set; }

    public static Test EmptyTest()
    {
        return new Test(TypingTestType.Minimalistic, string.Empty, false, string.Empty);
    }

    public static Test InitializeTest(TypingTestType testType, string text, string executorName)
    {
        return new Test(testType, text, true, executorName);
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