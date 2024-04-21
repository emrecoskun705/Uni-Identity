namespace UniIdentity.WebApp.Abstractions;

/// <summary>
/// Contains the API endpoint routes.
/// </summary>
public static class ApiRoutes
{
    /// <summary>
    /// Contains the client routes.
    /// </summary>
    public static class Clients
    {
        public const string Create = "clients/create";
    }

    /// <summary>
    /// Contains the realm routes.
    /// </summary>
    public static class Realms
    {
        public const string Create = "realms/create";
    }
}