using FluentValidation;
using Wallets.Domain;

namespace Wallets.Application.Validators;

public class GetWalletStocksPositionValidator : AbstractValidator<WalletStocksPosition>
{
    private readonly IWalletRepository _wallets;
    public GetWalletStocksPositionValidator(IWalletRepository wallets)
    {
        _wallets = wallets;
        
        RuleFor(x => x.WalletId)
            .NotEmpty().NotNull().WithMessage("Wallet ID is required.")
            .MustAsync(BeAValidGuidAsync).WithMessage("Invalid Wallet ID format.");
    }

    private async Task<bool> BeAValidGuidAsync(Guid guid, CancellationToken cancellationToken)
    {
        var wallet = await _wallets.GetAsync(guid, cancellationToken);
        return wallet != null;
    }
}