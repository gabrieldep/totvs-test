using FluentAssertions;

using Investiments.Application.Dtos;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Moq;

using Xunit;

namespace Investiments.RestApi.UnitTests;
public class UpdateLocalizationsControllerTests
{
    private readonly UpdateLocalizationsController _subject;

    private readonly Mock<ISender> _sender = new();

    public UpdateLocalizationsControllerTests()
    {
        _subject = new(_sender.Object);
    }

    [Fact]
    public async Task PutAsync_ShouldReturnNoContent()
    {
        // arrange
        var investimentId = Guid.NewGuid();
        var localizations = Array.Empty<InvestimentLocalizationData>();

        // act
        var result = await _subject.PutAsync(investimentId, localizations, CancellationToken.None);

        // assert
        result.Should().BeOfType<NoContentResult>();
    }
}
