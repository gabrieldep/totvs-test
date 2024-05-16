using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

using Stocks.Domain;

namespace Data.Repositories;

public class StockRepository(DbContext db) : IStockRepository
{
    public Task InsertAsync(Stock stock, CancellationToken cancellationToken)
    {
        return db.Set<Stock>().AddAsync(stock, cancellationToken).AsTask();
    }

    public async Task<IQueryable<Stock>> GetStocks(Guid walletId, CancellationToken cancellationToken)
    {
        var stocks = db.Set<Stock>().Where(s => s.WalletId == walletId);
        return await Task.FromResult(stocks);
    }
}
