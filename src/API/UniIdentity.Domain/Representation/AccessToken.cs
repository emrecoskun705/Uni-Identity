using System.Text.Json.Serialization;

namespace UniIdentity.Domain.Representation;

[Serializable]
public class AccessToken : IdToken
{
    [JsonPropertyName(UniJwtClaimNames.RealmAccess)]
    public Access? RealmAccess { get; set; }
    
    [JsonPropertyName(UniJwtClaimNames.ResourceAccess)]
    public Dictionary<string, Access>? ResourceAccess { get; set; }
    
    [JsonPropertyName(UniJwtClaimNames.Scope)]
    public string? Scope { get; set; }
    
    public override TokenType GetTokenType()
    {
        return TokenType.Access;
    }
    
    [Serializable]
    public class Access
    {
        [JsonPropertyName(UniJwtClaimNames.Roles)]
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
