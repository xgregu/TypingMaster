using CommandLine;

namespace WebViewApp;

public class CommandLineOptions
{
    [Value(0, MetaName = "url address",
        HelpText = "Url Address.",
        Required = true)]
    public string UrlAddress { get; set; } = "about:blank";
}