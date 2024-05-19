using FluentAssertions;
using Moq;
using Stocks.Application.DTOs;
using Stocks.Application.Services;
using Stocks.Domain;
using Wallets.Application.DTOs;

using Xunit;

namespace Wallets.Application.UnitTests;

public class GetWalletStocksPositionCommandHandler
{
    private readonly GetWalletStocksPositionHandler _subject;

    private readonly Mock<IStockRepository> _stocks = new();
    private readonly Mock<IStockPositionService> _stocksPosition = new();

    public GetWalletStocksPositionCommandHandler()
    {
        _subject = new GetWalletStocksPositionHandler(_stocks.Object, _stocksPosition.Object);
    }

    [Fact]
    public async Task Handle_ShouldGetValidStocksPositionValues()
    {
        // arrange
        var walletId = Guid.NewGuid();
        var stocksFromRepository = GetValidWalletStocks();
        var stocksPositionDto = GetValidStockPositionResponse();
        var expectedStockPositions = GetValidStockPositionResponseDto();

        var command = new GetWalletStocksPosition{WalletId = walletId};
        _stocksPosition.Setup(s =>
            s.GetStockPositionsAsync()).ReturnsAsync(stocksPositionDto.ToArray());

        _stocks.Setup(repo =>
                repo.GetStocks(walletId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(stocksFromRepository.AsQueryable());
        // act
        var response = await _subject.Handle(command, CancellationToken.None);

        // assert
        response.Should().BeEquivalentTo(expectedStockPositions);
    }
    
    private static List<Stock> GetValidWalletStocks() =>
    [
        new Stock { Code = "PETR4", Value = 2.1m, Amount = 5 },
        new Stock { Code = "PETR4", Value = 2.4m, Amount = 5 },
        new Stock { Code = "ITUB4", Value = 12.1m, Amount = 10 }
    ];
    
    private static List<StockPositionDTO> GetValidStockPositionResponse() =>
    [
        new StockPositionDTO { Code = "PETR4", Value = "33.1" },
        new StockPositionDTO { Code = "ITUB4", Value = "43.5" }
    ];

    private static List<StockPositionResponseDTO> GetValidStockPositionResponseDto() =>
    [
        new StockPositionResponseDTO { Code = "PETR4", Amount = 10, ValuePerQuota = 33.1m },
        new StockPositionResponseDTO { Code = "ITUB4", Amount = 10, ValuePerQuota = 43.5m }
    ];
}