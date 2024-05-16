using System.Net;

using FluentValidation;

using Microsoft.AspNetCore.Http;

namespace Core.RestApi.ExceptionHandlers;

public class ValidationExceptionHandler : BaseExceptionHandler
{
    public override async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is not ValidationException validationException)
        {
            return false;
        }

        return await HandlingExceptionAsync(
            httpContext,
            validationException,
            HttpStatusCode.BadRequest,
            "Validation error ocurred",
            new Dictionary<string, object?>
            {
                { "validationErrors", validationException.Errors.GroupBy(x => x.PropertyName) }
            },
            cancellationToken
        );
    }
}
