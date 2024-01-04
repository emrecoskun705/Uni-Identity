using MediatR;
using UniIdentity.Domain.Shared;

namespace UniIdentity.Application.Contracts.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;