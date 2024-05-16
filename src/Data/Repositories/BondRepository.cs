using Bonds.Domain;

using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class BondRepository(DbContext db) : IBondRepository
{
    public Task InsertAsync(Bond bond, CancellationToken cancellationToken)
    {
        return db.Set<Bond>().AddAsync(bond, cancellationToken).AsTask();
    }
}
