using FluentAssertions;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Moq;

using Wallets.RestApi;

using Xunit;

namespace Stocks.RestApi.UnitTests;

public class CreateWalletControllerTests
{
    private readonly CreateWalletController _subject;

    private readonly Mock<ISender> _sender = new();

    public CreateWalletControllerTests()
    {
        _subject = new(_sender.Object);
    }

    [Fact]
    public async Task PostAsync_ShouldReturnOkObject()
    {
        // arrange
        var request = new CreateWalletRequest();

        // act
        var result = await _subject.PostAsync(request, CancellationToken.None);

        // assert
        result.Should().BeOfType<OkObjectResult>();
    }
}
