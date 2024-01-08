using UniIdentity.Domain.Common;

namespace UniIdentity.Domain.Clients;

public sealed class ClientAttribute : BaseEntity<ClientId>
{
    public string Name { get; private set; }
    public string Value { get; private set; }
    
    public Client Client { get; set; }

    public ClientAttribute(
        ClientId id,
        string name,
        string value) : base(id)
    {
        Name = name;
        Value = value;
    }
    
}