namespace Core.ResultPattern;

public class DataResult<T>: Result, IDataResult<T> 
{
    protected DataResult(bool success) : base(success)
    {
    }

    protected DataResult(bool success, string message) : base(success, message)
    {
    }

    protected DataResult(T data, bool success) : base(success)
    {
        Data = data;
    }

    protected DataResult(T data, bool success, string message) : base(success, message)
    {
        Data = data;
    }

    public T? Data { get; }
}

public class SuccessDataResult<T> : DataResult<T> 
{
    public SuccessDataResult() : base(true)
    {
    }
    
    public SuccessDataResult(string message) : base(true, message)
    {
    }
    public SuccessDataResult(T data) : base(data, true)
    {
    }

    public SuccessDataResult(T data, string message) : base(data, true, message)
    {
    }
}

public class ErrorDataResult<T> : DataResult<T>
{
    public ErrorDataResult() : base(false)
    {
    }
    
    public ErrorDataResult(string message) : base(false, message)
    {
    }
    public ErrorDataResult(T data) : base(data, false)
    {
    }

    public ErrorDataResult(T data, string message) : base(data, false, message)
    {
    }
}