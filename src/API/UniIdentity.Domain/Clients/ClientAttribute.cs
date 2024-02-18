using UniIdentity.Domain.Clients.ValueObjects;
using UniIdentity.Domain.Common;

namespace UniIdentity.Domain.Clients;

public sealed class ClientAttribute : BaseEntity
{
    public ClientUniqueId UniqueId { get; private set; }
    public string Name { get; private set; }
    public string Value { get; private set; }
    
    public Client Client { get; set; }

    public ClientAttribute(
        ClientUniqueId uniqueId,
        string name,
        string value)
    {
        UniqueId = uniqueId;
        Name = name;
        Value = value;
    }
    
}