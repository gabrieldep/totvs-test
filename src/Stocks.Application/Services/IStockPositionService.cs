using Stocks.Application.DTOs;

namespace Stocks.Application.Services;

public interface IStockPositionService
{
    Task<StockPositionDTO[]?> GetStockPositionsAsync();
}