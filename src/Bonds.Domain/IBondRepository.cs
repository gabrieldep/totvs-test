namespace Bonds.Domain;
public interface IBondRepository
{
    Task InsertAsync(Bond bond, CancellationToken cancellationToken);
}
