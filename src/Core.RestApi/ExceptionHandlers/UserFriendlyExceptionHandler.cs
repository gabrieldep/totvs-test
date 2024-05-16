using System.Net;

using Core.Domain.Exceptions;

using Microsoft.AspNetCore.Http;

namespace Core.RestApi.ExceptionHandlers;

public class UserFriendlyExceptionHandler : BaseExceptionHandler
{
    public override async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is not UserFriendlyException userFriendlyException)
        {
            return false;
        }

        return await HandlingExceptionAsync(
            httpContext,
            userFriendlyException,
            HttpStatusCode.BadRequest,
            "An error ocurred",
            new Dictionary<string, object?>(),
            cancellationToken
        );
    }
}
