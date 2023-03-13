using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Data;
using Microsoft.Extensions.Logging;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.Wpf;

namespace WebViewApp.Views;

public partial class WebViewWindow : INotifyPropertyChanged
{
    private static readonly string BaseDir = AppDomain.CurrentDomain.BaseDirectory;
    private static readonly string WebView2Dir = Path.Combine(BaseDir, "WebView2");
    private static readonly string UserDataFolder = Path.Combine(WebView2Dir, "UserData");

    private readonly ILogger<WebViewWindow> _logger;
    private Uri _webViewSource = new("about:blank");
    private string _webViewTitle = string.Empty;

    public WebViewWindow(ILogger<WebViewWindow> logger)
    {
        _logger = logger;
        DataContext = this;

        InitializeComponent();
        InitializeWebView();


        Left = int.MinValue;
        Top = int.MinValue;
        
        Show();
    }

    public Uri WebViewSource
    {
        get => _webViewSource;
        set
        {
            _webViewSource = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(WebViewSource)));
        }
    }

    public string WebViewTitle
    {
        set
        {
            _webViewTitle = value;
            Dispatcher.Invoke(() => { Title = _webViewTitle; });
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private async void InitializeWebView()
    {
        _logger.LogInformation("Initialize WebView");

        _logger.LogInformation("WebViewWindow: InitializeWebView. BrowserExecutableFolder: {WebView2Dir}", WebView2Dir);
        _logger.LogInformation("WebViewWindow: InitializeWebView. UserDataFolder: {UserDataFolder}", UserDataFolder);

        try
        {
            WebView.CreationProperties = new CoreWebView2CreationProperties
            {
                BrowserExecutableFolder = WebView2Dir,
                UserDataFolder = UserDataFolder
            };

            WebView.CoreWebView2InitializationCompleted += WebViewOnCoreWebView2InitializationCompleted;
            var environment = await CoreWebView2Environment.CreateAsync(WebView2Dir, UserDataFolder);
            await WebView.EnsureCoreWebView2Async(environment);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "InitializeWebView");
        }
    }

    private void WebViewOnCoreWebView2InitializationCompleted(object? sender,
        CoreWebView2InitializationCompletedEventArgs e)
    {
        try
        {
            _logger.LogInformation("Initialize CoreWebView2");
            WebView.CoreWebView2.SourceChanged += (_, _) =>
                _logger.LogInformation("CoreWebView2 source changed: {WebViewSource}", WebView.Source.ToString());
            WebView.CoreWebView2.NavigationStarting += (_, _) =>
                _logger.LogInformation("CoreWebView2 navigation starting url: {WebViewSource}",
                    WebView.Source.ToString());
            WebView.CoreWebView2.NavigationCompleted += (_, _) =>
            {
                _logger.LogInformation("CoreWebView2 navigation completed url: {WebViewSource}",
                    WebView.Source.ToString());
                CenterWindowOnScreen();
            };

            WebView.CoreWebView2.Settings.AreDefaultContextMenusEnabled = false;
            WebView.CoreWebView2.Settings.IsGeneralAutofillEnabled = false;
            WebView.CoreWebView2.Settings.IsPasswordAutosaveEnabled = false;
            WebView.CoreWebView2.Settings.AreDevToolsEnabled = false;
            WebView.CoreWebView2.Settings.IsStatusBarEnabled = false;
            WebView.CoreWebView2.Settings.IsZoomControlEnabled = false;
            WebView.CoreWebView2.Settings.IsPinchZoomEnabled = false;
            WebView.CoreWebView2.Settings.IsSwipeNavigationEnabled = false;
            WebView.AllowDrop = false;
            WebView.AllowExternalDrop = false;

            SetUpSourceBinding();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "WebViewOnCoreWebView2InitializationCompleted");
        }

        Dispatcher.Invoke(Hide);
    }

    private void SetUpSourceBinding()
    {
        var sourceBinding = new Binding(nameof(WebViewSource))
        {
            Mode = BindingMode.TwoWay,
            UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
            Source = DataContext
        };
        WebView.SetBinding(WebView2.SourceProperty, sourceBinding);
    }
    
    private void CenterWindowOnScreen()
    {
        Dispatcher.Invoke(() =>
        {
            var screenWidth = SystemParameters.PrimaryScreenWidth;
            var screenHeight = SystemParameters.PrimaryScreenHeight;
            var windowWidth = Width;
            var windowHeight = Height;
            Left = screenWidth / 2 - windowWidth / 2;
            Top = screenHeight / 2 - windowHeight / 2;
            Show();
        });
    }
}