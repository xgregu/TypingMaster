using System.Reflection;

namespace TypingMaster.Shared;

public static class Constants
{
    private static readonly AssemblyName? AssemblyName = Assembly.GetEntryAssembly()?.GetName();

    public static readonly string AppFriendlyName = "Mistrz klawiatury";
    public static readonly string AppName = AssemblyName?.Name ?? AppFriendlyName.Replace(" ", "");
    public static readonly string TempPath = Path.Combine(Path.GetTempPath(), AppName);
    public static readonly string? Version = Assembly.GetEntryAssembly()?.GetName().Version?.ToString(3);
}