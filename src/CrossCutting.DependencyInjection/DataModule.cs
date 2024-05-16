using Bonds.Domain;

using Data.Repositories;

using Microsoft.Extensions.DependencyInjection;

using Stocks.Domain;

using Wallets.Domain;

namespace CrossCutting.DependencyInjection;

internal static class DataModule
{
    public static void AddDataModule(this IServiceCollection services)
    {
        services
            .AddScoped<IStockRepository, StockRepository>()
            .AddScoped<IBondRepository, BondRepository>()
            .AddScoped<IWalletRepository, WalletRepository>();
    }
}
