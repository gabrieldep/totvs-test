using MediatR;

using Wallets.Domain;

namespace Wallets.Application;
public record CreateWallet : IRequest<Guid>
{
    public string DisplayName { get; set; } = null!;
}

public class CreateWalletHandler(IWalletRepository wallets) : IRequestHandler<CreateWallet, Guid>
{
    public async Task<Guid> Handle(CreateWallet request, CancellationToken cancellationToken)
    {
        var wallet = new Wallet { DisplayName = request.DisplayName };

        await wallets.InsertAsync(wallet, cancellationToken);

        return wallet.Id;
    }
}