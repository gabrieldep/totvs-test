using FluentAssertions;

using FluentValidation;

using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

using Wallets.Application.Validators;
using Wallets.RestApi;
using Xunit;

namespace Stocks.RestApi.UnitTests;

public class GetWalletStockPositionControllerTests
{
    private readonly WalletStocksPositionController _subject;

    private readonly Mock<ISender> _sender = new();
    private readonly Mock<IValidator<WalletStocksPosition>> _validator = new();

    public GetWalletStockPositionControllerTests()
    {
        _subject = new(_sender.Object, _validator.Object);
    }

    [Fact]
    public async Task GetAsync_ShouldReturnNotFoundObject()
    {
        // arrange
        var request = Guid.NewGuid();

        // act
        var result = await _subject.GetAsync(request, CancellationToken.None);

        // assert
        result.Should().BeOfType<NotFoundResult>();
    }
}