using Microsoft.Extensions.DependencyInjection;

using Wallets.Application;

namespace CrossCutting.DependencyInjection;

internal static class WalletModule
{
    public static void AddWalletModule(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(CreateWallet).Assembly);
        });
    }
}