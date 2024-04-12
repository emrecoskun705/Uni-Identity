using FluentValidation;
using UniIdentity.Application.Core.Extensions;

namespace UniIdentity.Application.Clients.Commands.AddClientRequest;

internal sealed class AddClientRequestCommandValidator : AbstractValidator<AddClientRequestCommand>
{
    public AddClientRequestCommandValidator()
    {
        RuleFor(x => x.ClientKey.Value)
            .NotEmpty()
            .WithError(ValidationErrors.Client.ClientKeyRequired);

        RuleFor(x => x.RootUrl)
            .NotEmpty()
            .WithError(ValidationErrors.Client.RootUrlRequired);
    }
}