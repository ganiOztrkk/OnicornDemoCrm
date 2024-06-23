namespace Core.ResultPattern;

public class Result : IResult
{
    protected Result(bool success)
    {
        Success = success;
    }

    protected Result(bool success, string message) : this(success)
    {
        Message = message;
    }
    public bool Success { get; }
    public string? Message { get; }
}

public class SuccessResult : Result
{
    public SuccessResult() : base(true)
    {
    }

    public SuccessResult(string message) : base(true, message)
    {
    }
}

public class ErrorResult : Result
{
    public ErrorResult() : base(false)
    {
    }

    public ErrorResult(string message) : base(false, message)
    {
    }
}