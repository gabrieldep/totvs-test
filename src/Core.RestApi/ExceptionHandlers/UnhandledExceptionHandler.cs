using System.Net;

using Microsoft.AspNetCore.Http;

namespace Core.RestApi.ExceptionHandlers;

public class UnhandledExceptionHandler : BaseExceptionHandler
{
    public override async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        return await HandlingExceptionAsync(
            httpContext,
            exception,
            HttpStatusCode.InternalServerError,
            "An unexpected error ocurred",
            new Dictionary<string, object?>(),
            cancellationToken
        );
    }
}
