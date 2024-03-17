using UniIdentity.Domain.Shared;

// ReSharper disable once CheckNamespace
namespace UniIdentity.Domain;

public static partial class DomainErrors
{
    public static class ClientErrors
    {
        public static Error NotFound = Error.NotFound(
            "Client.NotFound",
            "The client with the specified identifier was not found");
        
    }
}