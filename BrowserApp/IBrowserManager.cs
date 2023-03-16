using System.Threading.Tasks;

namespace BrowserApp;

public interface IBrowserManager
{
    Task NavigateTo(string url);
    Task SetTitle(string title);
}