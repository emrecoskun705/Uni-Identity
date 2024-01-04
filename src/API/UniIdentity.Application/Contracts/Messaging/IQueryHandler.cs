using MediatR;
using UniIdentity.Domain.Shared;

namespace UniIdentity.Application.Contracts.Messaging;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery: IQuery<TResponse>
{
    
}