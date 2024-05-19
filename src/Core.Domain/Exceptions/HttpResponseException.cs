using System.Net;

namespace Core.Domain.Exceptions;

public class HttpResponseException : Exception
    {
    public HttpStatusCode StatusCode { get; }
    
    public HttpResponseException(HttpStatusCode statusCode)
    {
        StatusCode = statusCode;
    }

    public HttpResponseException(HttpStatusCode statusCode, string message) : base(message)
    {
        StatusCode = statusCode;
    }

    public HttpResponseException(HttpStatusCode statusCode, string message, Exception innerException) : base(message, innerException)
    {
        StatusCode = statusCode;
    }
}