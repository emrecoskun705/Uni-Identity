using UniIdentity.Application.Contracts.Messaging;

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