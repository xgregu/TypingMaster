namespace TypingMaster.Browser;

public interface IBrowserManager
{
    Task StartBrowser(string url);
    Task CloseBrowser();
}