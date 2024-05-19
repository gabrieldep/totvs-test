using System.Net;
using System.Text;
using Core.Domain.Exceptions;
using ExternalServices.HttpClientWrapper;
using Helper;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using Newtonsoft.Json;

using Stocks.Application.DTOs;
using Stocks.Application.Services;
using Stocks.Domain;

using Wallets.Domain;

using Xunit;

namespace Stocks.Application.UnitTests;

public class StockPositionServiceTests
{
    private readonly Mock<IDistributedCache> _cache = new();
    private readonly  Mock<IHttpClientWrapper> _httpClient = new();

    public StockPositionServiceTests()
    {
    }

    [Fact]
    public async Task GetStockPositionsAsync_ShouldReturnsCachedDataWhenAvailable()
    {
        // Arrange
        var cachedData = TestDataProvider.GetValidStockPositionResponse();
        _cache.Setup(c => c.GetAsync("StockPositions", default)).ReturnsAsync(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(cachedData)));

        var service = new StockPositionService(_httpClient.Object, _cache.Object);

        // Act
        var result = await service.GetStockPositionsAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(cachedData.Count, result.Length);
    }

    [Fact]
    public async Task GetStockPositionsAsync_ShouldRetriesOnApiFailure()
    {
        // Arrange
        _cache.Setup(c => c.GetAsync("StockPositions", CancellationToken.None))
            .ReturnsAsync(() => null);
        _httpClient.SetupSequence(c => c.GetAsync(It.IsAny<string>()))
            .ThrowsAsync(new HttpRequestException("Simulated API failure"));

        var service = new StockPositionService(_httpClient.Object, _cache.Object);

        // Act
        await Assert.ThrowsAsync<UserFriendlyException>(async () => await service.GetStockPositionsAsync());

        // Assert
        _httpClient.Verify(h => h.GetAsync(It.IsAny<string>()), Times.Exactly(3));
    }
}