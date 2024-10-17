using UniIdentity.Application.Contracts.Messaging;
using UniIdentity.Domain.Shared;

namespace UniIdentity.Application.Realms.Commands.AddRealmRequest;

public sealed record AddRealmRequestCommand : ICommand
{
    public string Name { get; }
    public bool Enabled { get; }

    public AddRealmRequestCommand(string name, bool enabled)
    {
        Name = name;
        Enabled = enabled;
    }
}