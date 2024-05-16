using MediatR;

using Stocks.Domain;

using Wallets.Domain;

namespace Stocks.Application;

public record CreateStock : IRequest<Guid>
{
    public Guid WalletId { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Value { get; set; }
    public string Code { get; set; } = null!;
    public int Amount { get; set; }
}

public class CreateStockHandler(IWalletRepository wallets, IStockRepository stocks) : IRequestHandler<CreateStock, Guid>
{
    public async Task<Guid> Handle(CreateStock request, CancellationToken cancellationToken)
    {
        await wallets.ThrowNotFoundWhenNotExistsAsync(request.WalletId, cancellationToken);

        var stock = new Stock
        {
            WalletId = request.WalletId,
            Description = request.Description,
            Value = request.Value,
            Code = request.Code,
            Amount = request.Amount
        };

        await stocks.InsertAsync(stock, cancellationToken);

        return stock.Id;
    }
}