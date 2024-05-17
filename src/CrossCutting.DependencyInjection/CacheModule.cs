using Microsoft.Extensions.DependencyInjection;

namespace CrossCutting.DependencyInjection;

internal static class CacheModule
{
    public static void AddCacheModule(this IServiceCollection services)
    {
        services.AddMemoryCache();
        services.AddDistributedMemoryCache();
    }
}