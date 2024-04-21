using System.Text.Json.Serialization;

namespace UniIdentity.Domain.Shared;

/// <summary>
/// Represents an error.
/// </summary>
public class Error : IEquatable<Error>
{
    /// <summary>
    /// The empty error instance.
    /// </summary>
    public static readonly Error None = new(string.Empty, string.Empty, ErrorType.Failure);

    /// <summary>
    /// The null value error instance.
    /// </summary>
    public static readonly Error NullValue = new("Error.NullValue", "The specified result value is null.", ErrorType.Failure);

    /// <summary>
    /// The condition not met error instance.
    /// </summary>
    public static readonly Error ConditionNotMet = new("Error.ConditionNotMet", "The specified condition was not met.", ErrorType.Failure);

    public static Error NotFound(string code, string description) => new Error(code, description, ErrorType.NotFound);
    public static Error Validation(string code, string description) => new Error(code, description, ErrorType.Validation);
    public static Error Failure(string code, string description) => new Error(code, description, ErrorType.Failure);
    
    /// <summary>
    /// Initializes a new instance of the <see cref="Error"/> class.
    /// </summary>
    /// <param name="code">The error code.</param>
    /// <param name="message">The error message.</param>
    private Error(string code, string message, ErrorType errorType)
    {
        Code = code;
        Message = message;
        ErrorType = errorType;
    }

    /// <summary>
    /// Gets the error code.
    /// </summary>
    public string Code { get; }

    /// <summary>
    /// Gets the error message.
    /// </summary>
    public string Message { get; }
    
    /// <summary>
    /// Gets the error type.
    /// </summary>
    [JsonIgnore]
    public ErrorType ErrorType { get; }

    public static implicit operator string(Error error) => error.Code;

    public static bool operator ==(Error? a, Error? b)
    {
        if (a is null && b is null)
        {
            return true;
        }

        if (a is null || b is null)
        {
            return false;
        }

        return a.Equals(b);
    }

    public static bool operator !=(Error? a, Error? b) => !(a == b);

    /// <inheritdoc />
    public virtual bool Equals(Error? other)
    {
        if (other is null)
        {
            return false;
        }

        return Code == other.Code && Message == other.Message;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj) => obj is Error error && Equals(error);

    /// <inheritdoc />
    public override int GetHashCode() => HashCode.Combine(Code, Message);

    /// <inheritdoc />
    public override string ToString() => Code;
}

public enum ErrorType
{
    Failure,
    NotFound,
    Validation,
    Conflict
}
