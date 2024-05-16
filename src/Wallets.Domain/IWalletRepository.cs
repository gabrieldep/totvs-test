namespace Wallets.Domain;
public interface IWalletRepository
{
    Task<bool> ExistsAsync(Guid walletId, CancellationToken cancellationToken);
    Task InsertAsync(Wallet wallet, CancellationToken cancellationToken);
}
