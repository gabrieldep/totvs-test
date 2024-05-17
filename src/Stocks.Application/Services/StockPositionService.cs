using System;
using System.Net.Http;
using System.Text.Json;
using Stocks.Application.DTOs;

namespace Stocks.Application.Services;

public class StockPositionService : IStockPositionService
{
    private readonly HttpClient _httpClient;
    private static readonly string _apiRoute = "api/stock-position/today";
        
    public StockPositionService(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }
        
    public async Task<StockPositionDTO[]> GetStockPositionsAsync()
    {
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
            Console.WriteLine($"Erro ao obter os dados da API: {ex.Message}");
            throw;
        }
    }
}