using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CrossCutting.DependencyInjection;
public static class RootModule
{
    public static void AddRootModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddWalletModule();
        services.AddBondModule();
        services.AddStockModule();
        services.AddDataModule();
        services.AddEntityFrameworkModule(configuration);
    }
}
