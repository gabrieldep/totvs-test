using Stocks.Domain;

using Moq;

using Wallets.Domain;

using Xunit;

namespace Stocks.Application.UnitTests;
public class CreateStockTests
{
    private readonly CreateStockHandler _subject;

    private readonly Mock<IWalletRepository> _wallets = new();
    private readonly Mock<IStockRepository> _stocks = new();

    public CreateStockTests()
    {
        _subject = new(_wallets.Object, _stocks.Object);
    }

    [Fact]
    public async Task Handle_ShouldInsertStock()
    {
        // arrange
        _wallets
            .Setup(x => x.ExistsAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var command = new CreateStock();

        // act
        await _subject.Handle(command, CancellationToken.None);

        // assert
        _stocks.Verify(
            x => x.InsertAsync(It.IsAny<Stock>(), It.IsAny<CancellationToken>()),
            Times.Once
        );
    }
}
