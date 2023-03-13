namespace Wkp.Navigation.Hubs;

public interface IWebAppHubClient
{
    Task Navigate(string url);
    Task Title(string title);
}