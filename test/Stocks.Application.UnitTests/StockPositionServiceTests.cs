using System.Net;
using System.Text;
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
    private readonly Mock<IDistributedCache> cache = new();
    private readonly  Mock<IHttpClientWrapper> _httpClient = new();

    public StockPositionServiceTests()
    {
    }

    [Fact]
    public async Task GetStockPositionsAsync_ShouldReturnsCachedDataWhenAvailable()
    {
        // Arrange
        var cachedData = TestDataProvider.GetValidStockPositionResponse();
        cache.Setup(c => c.GetAsync("StockPositions", default)).ReturnsAsync(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(cachedData)));

        var service = new StockPositionService(_httpClient.Object, cache.Object);

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
        cache.Setup(c => c.GetAsync("StockPositions", CancellationToken.None))
            .ReturnsAsync(() => null);
        var stocksPositionDto = TestDataProvider.GetValidStockPositionResponse();

        _httpClient.SetupSequence(c => c.GetAsync("http://localhost:3001/api/stock-position/today"))
            .ThrowsAsync(new HttpRequestException("Simulated API failure"))
            .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(stocksPositionDto), Encoding.UTF8,
                    "application/json")
            });

        var service = new StockPositionService(_httpClient.Object, cache.Object);

        // Act
        var result = await service.GetStockPositionsAsync();

        // Assert
        _httpClient.Verify(h => h.GetAsync(It.IsAny<string>()),
            Times.Exactly(3));
    }
}