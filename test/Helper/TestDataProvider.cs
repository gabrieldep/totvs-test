using Stocks.Application.DTOs;
using Stocks.Domain;

using Wallets.Application.DTOs;

public class TestDataProvider
{
    private const string Petrobras = "PETR4";
    private const string Itau = "ITUB4";
    
    public static List<Stock> GetValidWalletStocks() =>
    [
        new Stock { Code = Petrobras, Value = 2.1m, Amount = 5 },
        new Stock { Code = Petrobras, Value = 2.4m, Amount = 5 },
        new Stock { Code = Itau, Value = 12.1m, Amount = 10 }
    ];

    public static List<StockPositionDTO> GetValidStockPositionResponse() =>
    [
        new StockPositionDTO { Code = Petrobras, Value = "33.1" },
        new StockPositionDTO { Code = Itau, Value = "43.5" }
    ];

    public static List<StockPositionResponseDTO> GetValidStockPositionResponseDto() =>
    [
        new StockPositionResponseDTO { Code = Petrobras, Amount = 10, ValuePerQuota = 33.1m },
        new StockPositionResponseDTO { Code = Itau, Amount = 10, ValuePerQuota = 43.5m }
    ];
}