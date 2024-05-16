using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Stocks.Application.Services;

namespace CrossCutting.DependencyInjection;

internal static class HttpClientModule
{
    public static void AddHttpClientModule(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Mockoon");
        services.AddHttpClient<StockPositionService>(client =>
        {
            client.BaseAddress = new Uri(connectionString);
        });
    }
}