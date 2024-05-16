using MediatR;
using Stocks.Domain;

using Wallets.Application.DTOs;

namespace Wallets.Application;

public record GetWalletStocksPosition : IRequest<IEnumerable<StockPositionResponseDTO>>
{
    public Guid WalletId { get; set; } = Guid.Empty!;
}

public class GetWalletStocksPositionHandler(IStockRepository stocksRepository) : 
    IRequestHandler<GetWalletStocksPosition, IEnumerable<StockPositionResponseDTO>>
{
    public async Task<IEnumerable<StockPositionResponseDTO>> Handle(GetWalletStocksPosition request, CancellationToken cancellationToken)
    {
        var stocksPosition = await stocksRepository.GetStocks(request.WalletId, cancellationToken);
        var stockPositionDto = GetStockAndAmount(stocksPosition);
        
        return stockPositionDto;
    }

    private static IEnumerable<StockPositionResponseDTO> GetStockAndAmount(IEnumerable<Stock> stocks)
        => stocks.GroupBy(s => s.Code)
            .Select(g => new StockPositionResponseDTO()
            {
                Code = g.Key,
                Amount = g.Sum(s => s.Amount),
            });
}