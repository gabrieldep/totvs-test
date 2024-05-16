using Microsoft.EntityFrameworkCore;

using Wallets.Domain;

namespace Data.Repositories;
public class WalletRepository(DbContext db) : IWalletRepository
{
    public Task<bool> ExistsAsync(Guid walletId, CancellationToken cancellationToken)
    {
        return db.Set<Wallet>().AnyAsync(x => x.Id == walletId, cancellationToken);
    }

    public Task InsertAsync(Wallet wallet, CancellationToken cancellationToken)
    {
        return db.Set<Wallet>().AddAsync(wallet, cancellationToken).AsTask();
    }
}
