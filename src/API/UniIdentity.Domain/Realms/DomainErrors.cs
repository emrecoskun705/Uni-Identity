using UniIdentity.Domain.Shared;

// ReSharper disable once CheckNamespace
namespace UniIdentity.Domain;

public static partial class DomainErrors
{
    public static class RealmErrors
    {
        public static Error AlreadyExists = Error.NotFound(
            "Realm.Exists",
            "The realm with the specified identifier exists.");
    }
}