using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Helper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
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
        var stocksFromRepository = TestDataProvider.GetValidWalletStocks();
        var stocksPositionDto = TestDataProvider.GetValidStockPositionResponse();
        var expectedStockPositions = TestDataProvider.GetValidStockPositionResponseDto();

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
        var stockPositionResponseDTOs = actualStockPositions as StockPositionResponseDTO[] ?? actualStockPositions.ToArray();
        stockPositionResponseDTOs.Should().BeEquivalentTo(expectedStockPositions);
        stockPositionResponseDTOs[0].Total.Should().Be(331.0m);
        stockPositionResponseDTOs[^1].Total.Should().Be(435.0m);
        _stockRepository.Verify(repo => repo.GetStocks(walletId, It.IsAny<CancellationToken>()), Times.Once);
    }
}