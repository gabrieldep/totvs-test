using System.Text.Json;

using ExternalServices.HttpClientWrapper;

using Microsoft.Extensions.Caching.Distributed;
using Stocks.Application.DTOs;

namespace Stocks.Application.Services;

public class StockPositionService(IHttpClientWrapper httpClient, IDistributedCache cache) : IStockPositionService
{
    private readonly IHttpClientWrapper _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    private readonly IDistributedCache _cache = cache ?? throw new ArgumentNullException(nameof(cache));
    
    private const string CacheKey = "StockPositions";
    private static readonly TimeSpan CacheExpiration = TimeSpan.FromMinutes(5);
    private const string ApiRoute = "api/stock-position/today";
    private const int MaxRetries = 3;
    private const int RetryDelaySeconds = 5;

    public async Task<StockPositionDTO[]> GetStockPositionsAsync()
    {
        var cachedData = await _cache.GetStringAsync(CacheKey);
        if (!string.IsNullOrEmpty(cachedData))
            return JsonSerializer.Deserialize<StockPositionDTO[]>(cachedData);

        for (int attempt = 0; attempt < MaxRetries; attempt++)
        {
            try
            {
                var apiResponse = await GetStockPositionsFromApiAsync();
                await _cache.SetStringAsync(CacheKey, JsonSerializer.Serialize(apiResponse), new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = CacheExpiration
                });
                return apiResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao obter os dados da API (tentativa {attempt + 1}): {ex.Message}");

                if (attempt == MaxRetries - 1)
                    throw;

                await Task.Delay(TimeSpan.FromSeconds(RetryDelaySeconds));
            }
        }
        return null;
    }

    private async Task<StockPositionDTO[]> GetStockPositionsFromApiAsync()
    {
        var response = await _httpClient.GetAsync(ApiRoute);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<StockPositionDTO[]>(json);
    }
}