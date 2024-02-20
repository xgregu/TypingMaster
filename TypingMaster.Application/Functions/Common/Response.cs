namespace TypingMaster.Application.Functions.Common;

public abstract class Response<T>
{
    public T? Item { get; protected set; }
    public ResponseStatus Status { get; protected set; }
    public string Message { get; protected set; } = string.Empty;
}

public enum ResponseStatus
{
    Success = 0,
    NotFound = 1,
    Failed = 3,
    Error = 4
}