using UniIdentity.Domain.Shared;

// ReSharper disable once CheckNamespace
namespace UniIdentity.Application;

internal static partial class ValidationErrors
{
    internal static class Realm
    {
        internal static Error ClientKeyRequired => Error.Validation(
            "Realm.RealmIdRequired",
            "RealmId is required");
    }
}