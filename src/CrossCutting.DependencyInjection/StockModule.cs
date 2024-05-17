using Microsoft.Extensions.DependencyInjection;
using Stocks.Application;
using Stocks.Application.Services;

namespace CrossCutting.DependencyInjection;

internal static class StockModule
{
    public static void AddStockModule(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(CreateStock).Assembly);
        }).AddTransient<IStockPositionService, StockPositionService>();
    }
}
