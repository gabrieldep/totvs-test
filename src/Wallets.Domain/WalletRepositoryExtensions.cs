using Core.Domain.Exceptions;

namespace Wallets.Domain;
public static class WalletRepositoryExtensions
{
    public static async Task ThrowNotFoundWhenNotExistsAsync(this IWalletRepository wallets, Guid walletId, CancellationToken cancellationToken)
    {
        if (await wallets.ExistsAsync(walletId, cancellationToken))
        {
            return;
        }

        throw new NotFoundException();
    }
}
