using UniIdentity.Domain.Shared;

namespace UniIdentity.WebApp.Extensions;

public static class ResultExtensions
{
    public static IResult ToOk<TResult>(this Result<TResult> result)
        => result.HandleFailure(() => Results.Ok(result.Value));
    
    public static IResult ToOk(this Result result)
        => result.HandleFailure(() => Results.Ok());

    public static IResult ToCreated(this Result result)
        => result.HandleFailure(Results.Created);
    

    public static IResult ToProblemDetails(this Result result)
    {
        if (result.IsSuccess)
            throw new InvalidOperationException();

        var errors = result.Errors;
        return Results.Problem(
            statusCode: GetStatusCode(result.ResultType),
            title: GetTitle(result.ResultType),
            type: GetType(result.ResultType),
            extensions: new Dictionary<string, object?>()
            {
                { "errors", errors }
            }
        );
    }

    static int GetStatusCode(ErrorType errorType) =>
        errorType switch
        {
            ErrorType.Failure => StatusCodes.Status400BadRequest,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status500InternalServerError
        };

    static string GetTitle(ErrorType errorType) =>
        errorType switch
        {
            ErrorType.Failure => "Bad Request",
            ErrorType.NotFound => "Not Found",
            ErrorType.Validation => "Bad Request",
            ErrorType.Conflict => "Conflict",
            _ => "Server Failure"
        };

    static string GetType(ErrorType statusCode) =>
        statusCode switch
        {
            ErrorType.Validation => "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            ErrorType.Failure => "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            ErrorType.NotFound => "https://tools.ietf.org/html/rfc7231#section-6.5.4",
            ErrorType.Conflict => "https://tools.ietf.org/html/rfc7231#section-6.5.8",
            _ => "https://tools.ietf.org/html/rfc7231#section-6.6.1"
        };
    

    private static IResult HandleFailure(this Result result, Func<IResult> successAction)
    {
        return result.IsFailure ? result.ToProblemDetails() : successAction();
    }
}