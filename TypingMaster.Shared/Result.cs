namespace TypingMaster.Shared;

public class Result<TOk, TBad>
{
    private TBad _error;
    private TOk _ok;

    public Result(bool isOk, TOk ok, TBad bad)
    {
        IsOk = isOk;

        if (IsOk)
        {
            if (ok == null)
                throw new ArgumentNullException(nameof(ok),
                    "If IsOk flag is set to true parameter 'ok' needs to be non null");

            Value = ok;
        }
        else
        {
            if (bad == null)
                throw new ArgumentNullException(nameof(bad),
                    "If IsOk flag is set to false parameter 'bad' needs to be non null");

            Error = bad;
        }
    }

    public bool IsOk { get; }
    public bool IsError => !IsOk;

    public TBad Error
    {
        get
        {
            if (IsOk)
                throw new InvalidOperationException(
                    "Result has IsOk flag set to true only Value property is available");
            return _error;
        }
        private set => _error = value;
    }

    public TOk Value
    {
        get
        {
            if (!IsOk)
                throw new InvalidOperationException(
                    "Result has IsOk flag set to false only Error property is available");
            return _ok;
        }
        private set => _ok = value;
    }

    public Result<TNew, TBad> Map<TNew>(Func<TOk, TNew> map) where TNew : class
    {
        return IsOk
            ? new Result<TNew, TBad>(IsOk, map(Value), default)
            : new Result<TNew, TBad>(false, default, Error);
    }

    public TOk ValueWithDefault(Func<TBad, TOk> defaultCreator)
    {
        return IsOk ? Value : defaultCreator(Error);
    }

    public void Do(Action<TOk> action)
    {
        if (IsOk)
            action(Value);
    }
}

public class Result
{
    public static Result<T, TK> Failed<T, TK>(TK wrong)
    {
        return new Result<T, TK>(false, default, wrong);
    }

    public static Result<T, TK> Ok<T, TK>(T ok)
    {
        return new Result<T, TK>(true, ok, default);
    }
}