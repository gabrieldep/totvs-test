using Core.Domain;

using Wallets.Domain;

namespace Investiments.Domain;
public abstract class Investiment : Entity
{
    public abstract InvestimentType Type { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Value { get; set; }
    public Guid WalletId { get; set; }
    public Wallet Wallet { get; set; } = null!;
}
