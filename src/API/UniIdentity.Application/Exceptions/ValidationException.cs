using FluentValidation.Results;
using UniIdentity.Domain.Shared;

namespace UniIdentity.Application.Exceptions;

public class ValidationException : Exception
{
    public ValidationException(IEnumerable<ValidationFailure> failures): base("One or more validation failures has occurred.") =>
        Errors = failures
            .Distinct()
            .Select(failure => Error.Validation(failure.ErrorCode, failure.ErrorMessage))
            .ToList();

    /// <summary>
    /// Gets the validation errors.
    /// </summary>
    public IReadOnlyCollection<Error> Errors { get; }
}