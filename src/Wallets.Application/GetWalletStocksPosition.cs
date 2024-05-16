using MediatR;

using Wallets.Domain;

namespace Wallets.Application;

public record GetWalletStocksPosition : IRequest<Wallet>
{
    public Guid WalletId { get; set; } = Guid.Empty!;
}

public class GetWalletStocksPositionHandler(IWalletRepository wallets) : IRequestHandler<GetWalletStocksPosition, Wallet>
{
    public async Task<Wallet> Handle(GetWalletStocksPosition request, CancellationToken cancellationToken)
    {
        var wallet = await wallets.GetAsync(request.WalletId, cancellationToken);
        return wallet;
    }
}