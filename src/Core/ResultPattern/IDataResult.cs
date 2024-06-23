namespace Core.ResultPattern;

public interface IDataResult<T> : IResult
{
    public T? Data { get; }
}