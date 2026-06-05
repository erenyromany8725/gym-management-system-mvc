namespace GymManagement.BLL.Common;
public class Result
{
    protected Result(bool isSuccess, string? error, string? errorKey = null)
    {
        if (isSuccess && error is not null)
            throw new InvalidOperationException("Success result cannot have an error");

        if (!isSuccess && string.IsNullOrWhiteSpace(error))
            throw new InvalidOperationException("Failure result must have an error");

        IsSuccess = isSuccess;
        Error = error;
        ErrorKey = errorKey;
    }

    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public string? Error { get; }
    public string? ErrorKey { get; }

    public static Result Success() => new(true, null);

    public static Result Failure(string error, string? errorKey = null)
        => new(false, error, errorKey);
}

public sealed class Result<T> : Result
{
    private Result(
        bool isSuccess,
        T? value,
        string? error,
        string? errorKey = null)
        : base(isSuccess, error, errorKey)
    {
        Value = value;
    }

    public T? Value { get; }

    public static Result<T> Success(T value)
        => new(true, value, null);

    public static Result<T> Failure(
        string error,
        string? errorKey = null)
        => new(false, default, error, errorKey);
}