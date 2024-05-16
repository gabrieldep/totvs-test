using Microsoft.Extensions.DependencyInjection;

using Stocks.Application;

namespace CrossCutting.DependencyInjection;

internal static class StockModule
{
    public static void AddStockModule(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(CreateStock).Assembly);
        });
    }
}
