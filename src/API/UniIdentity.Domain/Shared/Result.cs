using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace UniIdentity.Domain.Shared;

public class Result
{
    protected internal Result(bool isSuccess, Error error)
    {
        if (isSuccess && error != Error.None)
        {
            throw new InvalidCastException();
        }

        if (!isSuccess && error == Error.None)
        {
            throw new InvalidCastException();
        }

        ResultType = error.ErrorType;
        IsSuccess = isSuccess;
        Errors = new List<Error>()
        {
            error
        };
    }

    private Result(ErrorType errorType, params Error[] errors)
    {
        ResultType = errorType;
        IsSuccess = false;
        Errors = new List<Error>(errors);
    }   
    
    
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    
    /// <summary>
    /// Gets the error type.
    /// </summary>
    [JsonIgnore]
    public ErrorType ResultType { get; }

    public IReadOnlyList<Error> Errors { get; }

    public static Result Success() => new(true, Error.None);
    
    public static Result Failure(Error error) => new(false, error);
    public static Result Validation(params Error[] error) => new(ErrorType.Validation, error);

    public static Result<T> Success<T>(T value) => new(value, true, Error.None);
    
    public static Result<T> Failure<T>(Error error) => new(default!, false, error);

    public static Result<T> Create<T>(T? value) =>
        value is not null ? Success(value) : Failure<T>(Error.NullValue);

}

public class Result<T> : Result
{
    private readonly T? _value;


    protected internal Result(T value, bool isSuccess, Error error) : base(isSuccess, error)
    {
        _value = value;
    }

    [NotNull]
    public T Value => IsSuccess
        ? _value!
        : throw new InvalidOperationException("The value of a failure result can not be accessed");

    public static implicit operator Result<T>(T? value) => Create(value);
}
