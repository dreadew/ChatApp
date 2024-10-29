namespace ChatApp.Core.Results;

public class BaseResult
{
  public bool IsSuccess => ErrorMessage == null;
  public string? ErrorMessage { get; protected set; }
  public int ErrorCode { get; protected set; }
  public static BaseResult Success() => new BaseResult();
  public static BaseResult Error(string errorMessage, int errorCode) =>
    new BaseResult { ErrorCode = errorCode, ErrorMessage = errorMessage };
}

public class BaseResult<T> : BaseResult
{
  public BaseResult() {}
  public T? Data { get; private set; }
  public static BaseResult<T> Success(T data) => new BaseResult<T> { Data = data };
  public new static BaseResult<T> Error(string errorMessage, int errorCode) => new BaseResult<T> { ErrorCode = errorCode, ErrorMessage = errorMessage };
}