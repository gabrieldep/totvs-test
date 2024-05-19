namespace ExternalServices.HttpClientWrapper;

public interface IHttpClientWrapper
{
    Task<HttpResponseMessage> GetAsync(string requestUri);
}