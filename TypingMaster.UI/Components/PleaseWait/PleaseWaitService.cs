namespace TypingMaster.UI.Components.PleaseWait;

public record PleaseWaitParameters
{
    public string Text { get; init; } = string.Empty;
    public string Color { get; init; } = string.Empty;
}

public interface IPleaseWaitService
{
    public string Text { get; }
    public string Color { get; }
    public bool IsVisible { get; }

    Task Show(PleaseWaitParameters parameters);
    Task Show();
    void Hide();

    event EventHandler<bool> VisibilityChanged;
}

public class PleaseWaitService(ILogger<PleaseWaitService> logger) : IPleaseWaitService
{
    public event EventHandler<bool>? VisibilityChanged;

    public string Text { get; private set; } = string.Empty;
    public string Color { get; private set; } = string.Empty;
    public bool IsVisible { get; private set; }

    public Task Show()
    {
        return Show(new PleaseWaitParameters
        {
            Text = string.Empty,
            Color = string.Empty
        });
    }

    public async Task Show(PleaseWaitParameters parameters)
    {
        logger.LogInformation("Show");

        Text = parameters.Text;
        Color = parameters.Color;

        VisibilityChanged?.Invoke(this, true);
        IsVisible = true;
        await Task.Delay(300);
    }

    public void Hide()
    {
        logger.LogInformation("Hide");
        VisibilityChanged?.Invoke(this, false);
        IsVisible = false;
    }
}