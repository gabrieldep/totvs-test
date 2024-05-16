using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stocks.Domain;
public interface IStockRepository
{
    Task InsertAsync(Stock stock, CancellationToken cancellationToken);
    Task<IQueryable<Stock>> GetStocks(Guid walletId, CancellationToken cancellationToken);
}
