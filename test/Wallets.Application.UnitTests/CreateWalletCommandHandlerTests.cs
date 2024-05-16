using Moq;

using Wallets.Domain;

using Xunit;

namespace Wallets.Application.UnitTests;
public class CreateWalletTests
{
    private readonly CreateWalletHandler _subject;

    private readonly Mock<IWalletRepository> _wallets = new();

    public CreateWalletTests()
    {
        _subject = new(_wallets.Object);
    }

    [Fact]
    public async Task Handle_ShouldInsertWallet()
    {
        // arrange

        var command = new CreateWallet();

        // act
        await _subject.Handle(command, CancellationToken.None);

        // assert
        _wallets.Verify(
            x => x.InsertAsync(It.IsAny<Wallet>(), It.IsAny<CancellationToken>()),
            Times.Once
        );
    }
}
