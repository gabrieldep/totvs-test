using Bonds.Application;

using Microsoft.Extensions.DependencyInjection;

namespace CrossCutting.DependencyInjection;

internal static class BondModule
{
    public static void AddBondModule(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(CreateBond).Assembly);
        });
    }
}
