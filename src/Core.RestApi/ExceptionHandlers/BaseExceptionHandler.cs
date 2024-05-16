using System.Net;
using System.Text.Json;

using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Core.RestApi.ExceptionHandlers;

public abstract class BaseExceptionHandler : IExceptionHandler
{
    private static readonly JsonSerializerOptions SerializerOptions = new() { PropertyNameCaseInsensitive = true, MaxDepth = 10 };

    public abstract ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken);

    protected static async Task<bool> HandlingExceptionAsync(HttpContext httpContext, Exception exception, HttpStatusCode statusCode, string title, IDictionary<string, object?> extensions, CancellationToken cancellationToken)
    {
        var details = new ProblemDetails
        {
            Status = (int)statusCode,
            Type = exception.GetType().Name,
            Title = title,
            Detail = exception.Message,
            Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}",
            Extensions = extensions
        };

        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = details.Status.Value;

        var serializeResult = JsonSerializer.Serialize(details, SerializerOptions);
        await httpContext.Response.WriteAsync(serializeResult, cancellationToken);

        return true;
    }
}
