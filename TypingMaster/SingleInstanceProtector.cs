namespace TypingMaster;

public class SingleInstanceProtector : IDisposable
{
    private readonly Mutex _mutex;
    private bool _hasHandle;

    public SingleInstanceProtector(string appGuid)
    {
        var mutexId = $"Global\\{{{appGuid}}}";
        _mutex = new Mutex(false, mutexId, out _);
    }

    public void Dispose()
    {
        if (_hasHandle)
            _mutex.ReleaseMutex();
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
}