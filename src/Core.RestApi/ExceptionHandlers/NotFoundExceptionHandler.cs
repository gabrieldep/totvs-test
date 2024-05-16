using System.Net;

using Core.Domain.Exceptions;

using Microsoft.AspNetCore.Http;

namespace Core.RestApi.ExceptionHandlers;
public class NotFoundExceptionHandler : BaseExceptionHandler
{
    public override async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is not NotFoundException notFoundException)
        {
            return false;
        }

        return await HandlingExceptionAsync(
            httpContext,
            notFoundException,
            HttpStatusCode.NotFound,
            "Not found error occurred",
            new Dictionary<string, object?>(),
            cancellationToken
        );
    }
}
