using System.Text.Json.Serialization;
using UniIdentity.Application.Tokens.Attributes;

namespace UniIdentity.Application.Tokens.Models;

[Serializable]
public class AccessToken : IdToken
{
    [JsonPropertyName(UniJwtClaimNames.RealmAccess), Token]
    public Access? RealmAccess { get; set; }

    [JsonPropertyName(UniJwtClaimNames.ResourceAccess), Token]
    public Dictionary<string, Access>? ResourceAccess { get; private set; }
    
    [JsonPropertyName(UniJwtClaimNames.Scope), Token]
    public string? Scope { get; set; }

    public override TokenType GetTokenType()
    {
        return TokenType.Access;
    }

    public void AddNewResourceAccess(string key, Access value)
    {
        ResourceAccess ??= new Dictionary<string, Access>();
        
        ResourceAccess.Add(key, value);
    }

    [Serializable]
    public class Access
    {
        [JsonPropertyName(UniJwtClaimNames.Roles), Token]
        public List<string> Roles = []; 
        
        public bool RoleExists(string role)
        {
            return Roles?.Contains(role) ?? false;
        }

        public void AddRole(string role)
        {
            Roles.Add(role);
        }
        
    }



}
