using Bonds.Domain;

using Moq;

using Wallets.Domain;

using Xunit;

namespace Bonds.Application.UnitTests;
public class CreateBondTests
{
    private readonly CreateBondHandler _subject;

    private readonly Mock<IWalletRepository> _wallets = new();
    private readonly Mock<IBondRepository> _bonds = new();

    public CreateBondTests()
    {
        _subject = new(_wallets.Object, _bonds.Object);
    }

    [Fact]
    public async Task Handle_ShouldInsertBond()
    {
        // arrange
        _wallets
            .Setup(x => x.ExistsAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var command = new CreateBond();

        // act
        await _subject.Handle(command, CancellationToken.None);

        // assert
        _bonds.Verify(
            x => x.InsertAsync(It.IsAny<Bond>(), It.IsAny<CancellationToken>()),
            Times.Once
        );
    }
}
