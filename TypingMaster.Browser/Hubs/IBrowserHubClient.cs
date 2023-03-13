namespace TypingMaster.Browser.Hubs;

public interface IWebAppHubClient
{
    Task Navigate(string url);
    Task Title(string title);
}