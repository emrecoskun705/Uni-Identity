using FluentValidation;
using FluentValidation.Results;
using MediatR;
using UniIdentity.Domain.Shared;

namespace UniIdentity.Application.Behaviours;

internal sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class, IRequest<TResponse>
    where TResponse : class
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next();
        }
        
        var context = new ValidationContext<TRequest>(request);
        
        List<ValidationFailure> validationErrors = _validators
            .Select(v => v.Validate(context))
            .SelectMany(result => result.Errors)
            .Where(f => f != null)
            .ToList();
        
        if (validationErrors.Any())
        {
            return CreateValidationErrorResponse(validationErrors);
        }
        
        return await next();
    }
    
    private TResponse CreateValidationErrorResponse(IEnumerable<ValidationFailure> validationErrors)
    {

        var errors = validationErrors.Distinct()
            .Select(failure => Error.Validation(failure.ErrorCode, failure.ErrorMessage))
            .ToArray();
        
        var response = Result.Validation(errors);
        
        return (response as TResponse)!;
    }

}