using System.Text.Json.Serialization;

namespace UniIdentity.Domain.Representation;

[Serializable]
public class AccessToken : IdToken
{
    [JsonPropertyName("realm_access")]
    public Access? RealmAccess { get; set; }
    
    [JsonPropertyName("resource_access")]
    public Dictionary<string, Access>? ResourceAccess { get; set; }
    
    [JsonPropertyName("scope")]
    public string? Scope { get; set; }
    
    public override TokenType GetTokenType()
    {
        return TokenType.Access;
    }
    
    [Serializable]
    public class Access
    {
        [JsonPropertyName("roles")]
        private HashSet<string>? _roles;
        public HashSet<string>? Roles
        {
            get => _roles;
            private set => _roles = value;
        }
        
        public bool RoleExists(string role)
        {
            return Roles?.Contains(role) ?? false;
        }

        public void AddRole(string role)
        {
            Roles ??= new();

            Roles.Add(role);
        }
    }
}
