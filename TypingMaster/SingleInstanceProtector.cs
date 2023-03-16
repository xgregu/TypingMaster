namespace TypingMaster;

public class SingleInstanceProtector : IDisposable
{
    private readonly string _appGuid;
    private bool _hasHandle;
    private readonly Mutex _mutex;

    public SingleInstanceProtector(string appGuid)
    {
        _appGuid = appGuid;
        var mutexId = $"Global\\{{{_appGuid}}}";
        _mutex = new Mutex(false, mutexId, out var createdNew);
    }

    public bool CheckOneInstanceRunning()
    {
        try
        {
            _hasHandle = _mutex.WaitOne(500, false);
        }
        catch (AbandonedMutexException)
        {
            _hasHandle = true;
        }

        return _hasHandle;
    }

    public void Dispose()
    {
        if (_hasHandle)
            _mutex.ReleaseMutex();
    }
}