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
    private readonly StockPositionService _service;
    private readonly Mock<IDistributedCache> _cache = new();
    private readonly  Mock<IHttpClientWrapper> _httpClient = new();

    public StockPositionServiceTests()
    {
        _service = new StockPositionService(_httpClient.Object, _cache.Object);
    }

    [Fact]
    public async Task GetStockPositionsAsync_ShouldReturnsCachedDataWhenAvailable()
    {
        // Arrange
        var cachedData = TestDataProvider.GetValidStockPositionResponse();
        _cache.Setup(c => c.GetAsync("StockPositions", default)).ReturnsAsync(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(cachedData)));

        // Act
        var result = await _service.GetStockPositionsAsync();

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

        // Act/Assert
        await Assert.ThrowsAsync<HttpResponseException>(async () => await _service.GetStockPositionsAsync());

        // Assert
        _httpClient.Verify(h => h.GetAsync(It.IsAny<string>()), Times.Exactly(3));
    }
}