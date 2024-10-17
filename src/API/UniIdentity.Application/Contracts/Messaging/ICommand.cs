using MediatR;
using UniIdentity.Domain.Shared;

namespace UniIdentity.Application.Contracts.Messaging;

/// <summary>
/// Represents the command interface.
/// </summary>
/// <typeparam name="TResponse">The command response type.</typeparam>
public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}

public interface ICommand : IRequest<Result>
{
    
}