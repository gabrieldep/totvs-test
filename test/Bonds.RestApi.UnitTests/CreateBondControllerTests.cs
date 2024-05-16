using FluentAssertions;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Moq;

using Xunit;

namespace Bonds.RestApi.UnitTests;

public class CreateBondControllerTests
{
    private readonly CreateBondController _subject;

    private readonly Mock<ISender> _sender = new();

    public CreateBondControllerTests()
    {
        _subject = new(_sender.Object);
    }

    [Fact]
    public async Task PostAsync_ShouldReturnOkObject()
    {
        // arrange
        var walletId = Guid.NewGuid();
        var request = new CreateBondRequest();

        // act
        var result = await _subject.PostAsync(walletId, request, CancellationToken.None);

        // assert
        result.Should().BeOfType<OkObjectResult>();
    }
}
