using Bonds.Domain;
using MediatR;

using Wallets.Domain;

namespace Bonds.Application;
public record CreateBond : IRequest<Guid>
{
    public Guid WalletId { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Value { get; set; }
    public DateTime Expiration { get; set; }
    public decimal AnnualInterestRate { get; set; }
}

public class CreateBondHandler(IWalletRepository wallets, IBondRepository bonds) : IRequestHandler<CreateBond, Guid>
{
    public async Task<Guid> Handle(CreateBond request, CancellationToken cancellationToken)
    {
        await wallets.ThrowNotFoundWhenNotExistsAsync(request.WalletId, cancellationToken);

        var bond = new Bond
        {
            WalletId = request.WalletId,
            Description = request.Description,
            Value = request.Value,
            Expiration = request.Expiration,
            AnnualInterestRate = request.AnnualInterestRate
        };

        await bonds.InsertAsync(bond, cancellationToken);

        return bond.Id;
    }
}
