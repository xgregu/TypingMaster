using System;
using System.Collections.Generic;
using CommandLine;
using Microsoft.Extensions.Logging;
using WebViewApp.Views;

namespace WebViewApp;


public interface IBrowserManager
{
    void StartBrowser(IEnumerable<string> arguments);
}

public class BrowserManager : IBrowserManager
{
    private readonly ILogger<BrowserManager> _logger;
    private readonly WebViewWindow _webViewWindow;

    public BrowserManager(ILogger<BrowserManager> logger, WebViewWindow webViewWindow)
    {
        _logger = logger;
        _webViewWindow = webViewWindow;
    }

    public void StartBrowser(IEnumerable<string> arguments)
    {
        _logger.LogInformation("Start browser | {Arguments}", arguments);

        Parser.Default
            .ParseArguments<CommandLineOptions>(arguments)
            .WithParsed(HandleParse);
    }

    private void HandleParse(CommandLineOptions commandLineOptions)
    {
        _logger.LogInformation("Command line parametr: {uri}", commandLineOptions.UrlAddress);
        _webViewWindow.WebViewSource = new UriBuilder(commandLineOptions.UrlAddress).Uri;
    }
}