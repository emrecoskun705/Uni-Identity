using UniIdentity.Domain.Shared;

namespace UniIdentity.Domain;

public static partial class DomainErrors
{
    /// <summary>
    /// Contains general errors.
    /// </summary>
    public static class General
    {
        public static Error UnProcessableRequest => Error.Failure(
            "General.UnProcessableRequest",
            "The server could not process the request.");

        public static Error ServerError => Error.Failure("General.ServerError", "The server encountered an error.");
    }
}