using Bonds.Domain;
using Data.Repositories;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

using Stocks.Application.Services;
using Stocks.Domain;
using Wallets.Application;
using Wallets.Application.Validators;
using Wallets.Domain;

namespace CrossCutting.DependencyInjection;

internal static class DataModule
{
    public static void AddDataModule(this IServiceCollection services)
    {
        services
            .AddScoped<IStockRepository, StockRepository>()
            .AddScoped<IBondRepository, BondRepository>()
            .AddScoped<IWalletRepository, WalletRepository>()
            .AddTransient<IValidator<WalletStocksPosition>, GetWalletStocksPositionValidator>();
    }
}
