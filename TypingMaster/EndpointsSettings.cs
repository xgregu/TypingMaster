namespace TypingMaster;

public class EndpointsSettings
{
    public const string SectionName = "Endpoints";

    public string ApiEndpoint { get; init; } = string.Empty;
    public string HubEndpoint { get; init; } = string.Empty;
}