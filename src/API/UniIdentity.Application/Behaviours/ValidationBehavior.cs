using FluentValidation;
using MediatR;
using UniIdentity.Application.Contracts.Messaging;
using UniIdentity.Application.Exceptions;
using UniIdentity.Domain.Shared;

namespace UniIdentity.Application.Behaviours;

internal sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> 
    where TRequest : IBaseCommand where TResponse : Result
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
        
        var validationErrors = _validators
            .Select(v => v.Validate(context))
            .SelectMany(result => result.Errors)
            .Where(f => f != null)
            .ToList();
        
        if (validationErrors.Any())
        {
            throw new Exceptions.ValidationException(validationErrors);
        }
        
        return await next();
    }
}