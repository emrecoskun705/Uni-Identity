using FluentValidation;
using UniIdentity.Application.Core.Extensions;

namespace UniIdentity.Application.Realms.Commands.AddRealmRequest;

public class AddRealmRequestCommandValidator : AbstractValidator<AddRealmRequestCommand>
{
    public AddRealmRequestCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithError(ValidationErrors.Realm.ClientKeyRequired);
    }
}