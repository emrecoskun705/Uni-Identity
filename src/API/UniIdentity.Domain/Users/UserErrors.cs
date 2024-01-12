using UniIdentity.Domain.Shared;

namespace UniIdentity.Domain.Users;

public static class UserErrors
{
    public static Error NotFound = Error.NotFound(
        "User.NotFound",
        "The user with the specified identifier was not found");

    public static Error InvalidCredentials = Error.Failure(
        "User.InvalidCredentials",
        "The provided credentials were invalid");
    
    public static Error UsernameExists = Error.Failure(
        "User.UsernameExists", 
        "Username exists");
    
    public static Error EmailExists = Error.Failure(
        "User.EmailExists", 
        "Email exists");
    
    public static Error NotActive = Error.Failure(
        "User.NotActive", 
        "User is not an active user");
}