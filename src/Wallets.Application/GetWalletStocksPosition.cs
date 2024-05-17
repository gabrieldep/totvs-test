using MediatR;
using Stocks.Application.Services;
using Stocks.Domain;
using Wallets.Application.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace Wallets.Application
{
    public record GetWalletStocksPosition : IRequest<IEnumerable<StockPositionResponseDTO>>
    {
        public Guid WalletId { get; set; } = Guid.Empty!;
    }

    public class GetWalletStocksPositionHandler : IRequestHandler<GetWalletStocksPosition, IEnumerable<StockPositionResponseDTO>>
    {
        private readonly IStockRepository _stocksRepository;
        private readonly StockPositionService _stockPositionService;

        public GetWalletStocksPositionHandler(IStockRepository stocksRepository, StockPositionService stockPositionService)
        {
            _stocksRepository = stocksRepository ?? throw new ArgumentNullException(nameof(stocksRepository));
            _stockPositionService = stockPositionService ?? throw new ArgumentNullException(nameof(stockPositionService));
        }

        public async Task<IEnumerable<StockPositionResponseDTO>> Handle(GetWalletStocksPosition request, CancellationToken cancellationToken)
        {
            var stocksPositionQueryable = await _stocksRepository.GetStocks(request.WalletId, cancellationToken);
            
            var stocksPositionDto = GetStockAndAmount(stocksPositionQueryable).ToArray();

            var stocksValue = await _stockPositionService.GetStockPositionsAsync();

            foreach (var t in stocksPositionDto)
            {
                var stockValue = stocksValue
                    .FirstOrDefault(sv => sv.Code == t.Code);
                t.ValuePerQuota = ConvertToDecimal(stockValue?.Value);
            }
            
            return stocksPositionDto;
        }

        private static IEnumerable<StockPositionResponseDTO> GetStockAndAmount(IEnumerable<Stock> stocks)
            => stocks.GroupBy(s => s.Code)
                .Select(g => new StockPositionResponseDTO()
                {
                    Code = g.Key,
                    Amount = g.Sum(s => s.Amount),
                });
        
        private static decimal ConvertToDecimal(string? value)
        {
            value = value?.Replace(',', '.');
            if (decimal.TryParse(value, out decimal result))
                return result;
            throw new ArgumentException($"Failed to convert '{value}' to decimal.");
        }
    }
}