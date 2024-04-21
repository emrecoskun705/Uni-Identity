using UniIdentity.Domain.Shared;

// ReSharper disable once CheckNamespace
namespace UniIdentity.Domain;

public static partial class DomainErrors
{
    public static class RealmErrors
    {
        public static Error AlreadyExists = Error.Failure(
            "Realm.Exists",
            "The realm with the specified identifier exists.");
        
        public static Error NotFound = Error.NotFound(
            "Realm.NotFound",
            "The realm is not found.");
    }
}