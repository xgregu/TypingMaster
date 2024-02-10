namespace TypingMaster;

public class SingleInstanceProtector : IDisposable
{
    private bool _hasHandle;
    private readonly Mutex _mutex;

    public SingleInstanceProtector(string appGuid)
    {
        var mutexId = $"Global\\{{{appGuid}}}";
        _mutex = new Mutex(false, mutexId, out _);
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