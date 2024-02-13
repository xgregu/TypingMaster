using TypingMaster.Shared.Dtos;

namespace TypingMaster.UI.Components.Models;

public class TestInProgress
{
    private TestInProgress(TypingTextDto text)
    {
        Text = text;
    }

    public TypingTextDto Text { get; }
    public string CurrentText { get; private set; }
    public int CompletionPercentage { get; private set; }
    public bool IsStarted { get; private set; }
    public int CorrectClicks => TotalClicks - InorrectClicks;
    public int InorrectClicks { get; private set; }
    public int TotalClicks { get; private set; }
    public DateTime StartTime { get; private set; }
    public DateTime EndTime { get; private set; }
    public bool IsComplete { get; private set; }
    public TimeSpan CurrentTestTime => IsStarted ? (IsComplete ? EndTime : DateTime.Now) - StartTime : default;

    public static TestInProgress InitializeTest(TypingTextDto text) => new( text);
    public static TestInProgress EmptyTest() => new(new TypingTextDto(long.MinValue, string.Empty, new TypingLevelDto(long.MinValue, string.Empty, 0)));

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

        if (!Text.Text.StartsWith(newText))
        {
            if (CurrentText?.Length - newText?.Length == -1)
                InorrectClicks++;

            newTextIsCorrect = false;
        }

        CurrentText = newText;

        if (newTextIsCorrect)
            CompletionPercentage = CurrentText.Length * 100 / Text.Text.Length;

        if (Text.Text.Equals(CurrentText))
            EndTest();

        return newTextIsCorrect;
    }

    public void StartTest()
    {
        IsStarted = true;
        StartTime = DateTime.Now;
    }

    private void EndTest()
    {
        IsComplete = true;
        EndTime = DateTime.Now;
    }
}

public static class TestInProgressExtensions
{
    public static CreateTestRequest EndTest(this TestInProgress testInProgress, string executorName) => new(executorName,
        testInProgress.StartTime, testInProgress.EndTime, testInProgress.TotalClicks,
        testInProgress.Text.Id);
}