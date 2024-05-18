using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Stocks.Application.DTOs;
using Stocks.Application.Services;
using Stocks.Domain;
using Wallets.Application;
using Wallets.Application.DTOs;
using Wallets.Application.Validators;
using Wallets.RestApi;
using Xunit;

namespace Stocks.RestApi.UnitTests;

public class GetWalletStockPositionControllerTests
{
    private readonly GetWalletStocksPositionController _subject;

    private readonly Mock<ISender> _sender = new();
    private readonly Mock<IValidator<WalletStocksPosition>> _validator = new();
    private readonly Mock<IStockRepository> _stockRepository = new();
    private readonly Mock<IStockPositionService> _stockPositionService = new();

    public GetWalletStockPositionControllerTests()
    {
        _subject = new(_sender.Object, _validator.Object);
    }

    [Fact]
    public async Task GetAsync_ShouldReturnNotFoundObject()
    {
        // arrange
        _validator.Setup(v => 
                v.ValidateAsync(It.IsAny<WalletStocksPosition>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult(new List<ValidationFailure>(){new ValidationFailure()}));
        
        var request = Guid.NewGuid();

        // act
        var result = await _subject.GetAsync(request, CancellationToken.None);

        // assert
        result.Should().BeOfType<NotFoundObjectResult>();
    }
    
    [Fact]
    public async Task GetAsync_ShouldReturnOkObjectResult()
    {
        // Arrange
        var walletId = Guid.NewGuid();
        var stocksFromRepository = GetValidWalletStocks();
        var stocksPositionDto = GetValidStockPositionResponse();
        var expectedStockPositions = GetExpectedResponse();

        _validator.Setup(v =>
                v.ValidateAsync(It.IsAny<WalletStocksPosition>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        _stockPositionService.Setup(s =>
            s.GetStockPositionsAsync()).ReturnsAsync(stocksPositionDto.ToArray());

        _stockRepository.Setup(repo =>
                repo.GetStocks(walletId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(stocksFromRepository.AsQueryable());

        var subject = new GetWalletStocksPositionHandler(_stockRepository.Object, _stockPositionService.Object);

        // Act
        var actualStockPositions = await subject.Handle(new GetWalletStocksPosition { WalletId = walletId }, CancellationToken.None);

        // Assert
        var stockPositionResponseDtos = actualStockPositions as StockPositionResponseDTO[] ?? actualStockPositions.ToArray();
        stockPositionResponseDtos.Should().BeEquivalentTo(expectedStockPositions);
        stockPositionResponseDtos.First().Total.Should().Be(331.0m);
        stockPositionResponseDtos.Last().Total.Should().Be(435.0m);
        _stockRepository.Verify(repo => repo.GetStocks(walletId, It.IsAny<CancellationToken>()), Times.Once);
    }


    private static List<Stock> GetValidWalletStocks() =>
    [
        new() { Code = "PETR4", Value = 2.1m, Amount = 5 },
        new() { Code = "PETR4", Value = 2.4m, Amount = 5 },
        new() { Code = "ITUB4", Value = 12.1m, Amount = 10 }
    ];
    
    private static List<StockPositionDTO> GetValidStockPositionResponse() =>
    [
        new() { Code = "PETR4", Value = "33.1" },
        new() { Code = "ITUB4", Value = "43.5" }
    ];

    private static List<StockPositionResponseDTO> GetExpectedResponse() =>
    [
        new StockPositionResponseDTO { Code = "PETR4", Amount = 10, ValuePerQuota = 33.1m },
        new StockPositionResponseDTO { Code = "ITUB4", Amount = 10, ValuePerQuota = 43.5m }
    ];
}