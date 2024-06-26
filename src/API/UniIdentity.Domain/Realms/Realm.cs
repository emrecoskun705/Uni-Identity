﻿using UniIdentity.Domain.Clients.Repositories;
using UniIdentity.Domain.Common;
using UniIdentity.Domain.RealmAttributes;
using UniIdentity.Domain.RealmAttributes.Repositories;
using UniIdentity.Domain.Realms.Consts;
using UniIdentity.Domain.Realms.Enums;
using UniIdentity.Domain.Scopes;
using UniIdentity.Domain.Scopes.Repositories;

namespace UniIdentity.Domain.Realms;

/// <summary>
/// Represents a realm within the identity system, providing management capabilities for users, applications, roles, and groups.
/// </summary>
/// <remarks>
/// Realms serve as spaces where various objects, such as users, applications, roles, and groups, are managed. Users typically belong to and authenticate within a specific realm.
/// </remarks>
public sealed class Realm : BaseEntity
{
    private const int DefaultLifeSpan = 60;

    /// <summary>
    /// Gets the unique identifier of the realm.
    /// </summary>
    public RealmId Id { get; private set; }

    /// <summary>
    /// Gets or sets the lifespan of access tokens issued for this realm, in seconds.
    /// </summary>
    public int AccessTokenLifeSpan { get; private set; }

    /// <summary>
    /// Gets or sets the maximum single sign-on (SSO) lifespan for this realm, in seconds.
    /// </summary>
    public int SsoMaxLifeSpan { get; private set; }

    /// <summary>
    /// Gets or sets the name of the realm.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Gets or sets the SSL requirement level for communications with this realm.
    /// </summary>
    public SslRequirement SslRequirement { get; private set; }

    /// <summary>
    /// Gets or sets a value indicating whether the realm is enabled.
    /// </summary>
    public bool Enabled { get; private set; }

    /// <summary>
    /// Gets or sets a value indicating whether email verification is required for this realm.
    /// </summary>
    public bool VerifyEmail { get; private set; }

    // Private constructor to enforce creation through factory method
    private Realm(
        RealmId id,
        int accessTokenLifeSpan,
        int ssoMaxLifeSpan,
        string name,
        SslRequirement sslRequirement,
        bool enabled,
        bool verifyEmail)
    {
        Id = id;
        AccessTokenLifeSpan = accessTokenLifeSpan;
        SsoMaxLifeSpan = ssoMaxLifeSpan;
        Name = name;
        SslRequirement = sslRequirement;
        Enabled = enabled;
        VerifyEmail = verifyEmail;
    }

    /// <summary>
    /// Creates a new realm.
    /// </summary>
    /// <param name="name">The name of the realm.</param>
    /// <param name="enabled">A value indicating whether the realm is enabled.</param>
    /// <returns>A new <see cref="Realm"/> instance with default attributes.</returns>
    public static Realm Create(
        string name,
        bool enabled)
    {
        var realm = new Realm(new RealmId(name), DefaultLifeSpan, DefaultLifeSpan, name, SslRequirement.None, enabled,
            false);
        return realm;
    }

    /// <summary>
    /// Retrieves the signature algorithm configured for this realm.
    /// </summary>
    /// <param name="getRealmAttributeRepository">The repository for retrieving realm attributes.</param>
    /// <param name="cancellationToken">A token to cancel the asynchronous operation (optional).</param>
    /// <returns>The signature algorithm associated with this realm, or <c>null</c> if not found.</returns>
    /// <remarks>
    /// This method asynchronously retrieves the signature algorithm configured for this realm.
    /// </remarks>
    public async Task<string?> GetSignatureAlgorithm(
        IGetRealmAttributeRepository getRealmAttributeRepository, CancellationToken cancellationToken = default)
    {
        return (await getRealmAttributeRepository.GetByNameAsync(Id, RealmAttributeName.SignatureAlgorithm,
            cancellationToken)).Value;
    }

    /// <summary>
    /// Creates a new realm attribute with the specified name and value.
    /// </summary>
    /// <param name="name">The name of the realm attribute.</param>
    /// <param name="value">The value of the realm attribute.</param>
    /// <returns>A new instance of <see cref="RealmAttribute"/>.</returns>
    public RealmAttribute CreateAttribute(string name, string value)
    {
        return new RealmAttribute(Id, name, value);
    }

    #region Adding default scopes

    /// <summary>
    /// Adds scopes for realm and also determines which scopes are default.
    /// </summary>
    /// <param name="addScopeRepository">The repository for adding scopes.</param>
    /// <param name="addDefaultScopeRepository">The repository for adding default scopes.</param>
    public IReadOnlyList<Scope> AddScopes(
        IAddScopeRepository addScopeRepository)
    {
        var scopes = new List<Scope>();
        foreach (var scope in DefaultScopes)
        {
            var addScope = Scope.Create(
                scope.Name,
                "openid-connect",
                Id,
                scope.Description
            );

            addScopeRepository.Add(addScope);
            scopes.Add(addScope);
        }

        return scopes;
    }

    public IReadOnlyList<DefaultScope> AddDefaultScopes(IAddDefaultScopeRepository addDefaultScopeRepository, IReadOnlyList<Scope> scopes)
    {
        var defaultScopes = new List<DefaultScope>();
        foreach (var scope in scopes)
        {
            var isDefault = DefaultScopes.First(x => x.Name == scope.Name).IsDefault;
            var defaultScope = new DefaultScope(Id, scope.Id, isDefault);
            
            addDefaultScopeRepository.Add(defaultScope);
            defaultScopes.Add(defaultScope);
        }

        return defaultScopes;
    }
    

    /// <summary>
    /// Adds a default scope to the realm. These configuration is used when a new client is created and adds to client's default scopes.
    /// </summary>
    /// <param name="addDefaultScopeRepository">The repository for adding default scopes.</param>
    /// <param name="scope">The scope entity to add as a default.</param>
    /// <param name="isDefault">Specifies whether the scope is a default scope.</param>
    private void AddDefaultScope(IAddDefaultScopeRepository addDefaultScopeRepository, Scope scope, bool isDefault)
    {
        var addDefaultScope = new DefaultScope(Id, scope.Id, isDefault);
        addDefaultScopeRepository.Add(addDefaultScope);
    }
    

    internal static readonly List<ScopeDto> DefaultScopes =
    [
        new ScopeDto("offline_access",
            "OpenID Connect built-in scope: offline_access"),

        new ScopeDto("profile", "OpenID Connect built-in scope: profile", true),
        new ScopeDto("email", "OpenID Connect built-in scope: email", true),
        new ScopeDto("address", "OpenID Connect built-in scope: address"),
        new ScopeDto("phone", "OpenID Connect built-in scope: phone"),
        new ScopeDto("roles",
            "OpenID Connect scope for add user roles to the access token", true),

        new ScopeDto("web-origins",
            "OpenID Connect scope for add allowed web origins to the access token", true),

        new ScopeDto("microprofile-jwt",
            "Microprofile - JWT built-in scope")
    ];

    internal class ScopeDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDefault { get; set; }

        public ScopeDto(string name, string description, bool isDefault = false)
        {
            Name = name;
            Description = description;
            IsDefault = isDefault;
        }
    }

    #endregion
}