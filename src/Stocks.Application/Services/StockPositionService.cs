using System;
using System.Net.Http;
using System.Text.Json;
using Stocks.Application.DTOs;

namespace Stocks.Application.Services;

public class StockPositionService : IStockPositionService
{
    private readonly HttpClient _httpClient;
    private static readonly string _apiRoute = "api/stock-position/today";
    private const int MaxRetries = 3;
    private const int RetryDelaySeconds = 5;
    
    public StockPositionService(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }
        
    public async Task<StockPositionDTO[]> GetStockPositionsAsync()
    {
        for (int attempt = 0; attempt < MaxRetries; attempt++)
            try
            {
                var response = await _httpClient.GetAsync(_apiRoute);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonSerializer.Deserialize<StockPositionDTO[]>(json);

                return apiResponse;
            }
            catch (Exception ex)
            {
                if (attempt == MaxRetries - 1)
                    throw;

                Console.WriteLine($"Erro ao obter os dados da API (tentativa {attempt + 1}): {ex.Message}");
                await Task.Delay(TimeSpan.FromSeconds(RetryDelaySeconds));
            }

        return null;
    }
}