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
        var stocksFromRepository = TestDataProvider.GetValidWalletStocks();
        var stocksPositionDto = TestDataProvider.GetValidStockPositionResponse();
        var expectedStockPositions = TestDataProvider.GetValidStockPositionResponseDto();

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
}