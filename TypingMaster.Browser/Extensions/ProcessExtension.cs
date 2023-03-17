using System.Diagnostics;

namespace TypingMaster.Browser.Extensions;

public static class ProcessExtension
{
    public static bool IsProcessRunning(this Process process)
    { 
        var processes = Process.GetProcessesByName(process.ProcessName);
        
        Process? processToReturn = null;
        try
        {
            foreach (var p in processes)
            {
                processToReturn = p;
                break;
            }
            processes.ToList().ForEach(x => x.Dispose());
        }
        catch (Exception)
        {
            processes.ToList().ForEach(x => x.Dispose());
        }

        return processToReturn is not null; 
    }
}