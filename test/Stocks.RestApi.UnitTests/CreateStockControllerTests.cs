using FluentAssertions;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Moq;

using Xunit;

namespace Stocks.RestApi.UnitTests;

public class CreateStockControllerTests
{
    private readonly CreateStockController _subject;

    private readonly Mock<ISender> _sender = new();

    public CreateStockControllerTests()
    {
        _subject = new(_sender.Object);
    }

    [Fact]
    public async Task PostAsync_ShouldReturnOkObject()
    {
        // arrange
        var walletId = Guid.NewGuid();
        var request = new CreateStockRequest();

        // act
        var result = await _subject.PostAsync(walletId, request, CancellationToken.None);

        // assert
        result.Should().BeOfType<OkObjectResult>();
    }
}
