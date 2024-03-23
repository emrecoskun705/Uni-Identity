using UniIdentity.Domain.Shared;

// ReSharper disable CheckNamespace

namespace UniIdentity.Application;

internal static partial class ValidationErrors
{
    internal static class Client
    {
        internal static Error ClientKeyRequired => Error.Validation(
            "Client.ClientKeyRequired",
            "ClientKey is required");
        
        internal static Error RootUrlRequired => Error.Validation(
            "Client.ClientKeyRequired",
            "ClientKey is required");
    }
}