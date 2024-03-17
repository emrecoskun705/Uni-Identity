using UniIdentity.Domain.Clients.ValueObjects;
using UniIdentity.Domain.Common;

namespace UniIdentity.Domain.ClientAttributes;

public sealed class ClientAttribute : BaseEntity
{
    public ClientId Id { get; private set; }
    public string Name { get; private set; }
    public string Value { get; private set; }
    
    internal ClientAttribute(
        ClientId id,
        string name,
        string value)
    {
        Id = id;
        Name = name;
        Value = value;
    }
    
}