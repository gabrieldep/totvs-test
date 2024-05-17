using System.Text.Json.Serialization;

namespace Stocks.Application.DTOs;

public class StockPositionDTO
{
    [JsonPropertyName("code")]
    public string Code { get; set; }
    
    [JsonPropertyName("value")]
    public string Value { get; set; }
}