using UniIdentity.Domain.Common;

namespace UniIdentity.Domain.Clients;

public sealed class ClientAttribute : BaseEntity
{
    public ClientId Id { get; private set; }
    public string Name { get; private set; }
    public string Value { get; private set; }
    
    public Client Client { get; set; }

    public ClientAttribute(
        ClientId id,
        string name,
        string value)
    {
        Id = id;
        Name = name;
        Value = value;
    }
    
}