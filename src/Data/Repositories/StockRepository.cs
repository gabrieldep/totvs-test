using Microsoft.EntityFrameworkCore;

using Stocks.Domain;

namespace Data.Repositories;

public class StockRepository(DbContext db) : IStockRepository
{
    public Task InsertAsync(Stock stock, CancellationToken cancellationToken)
    {
        return db.Set<Stock>().AddAsync(stock, cancellationToken).AsTask();
    }
}
