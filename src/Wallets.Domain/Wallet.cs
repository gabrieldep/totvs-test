using Core.Domain;

namespace Wallets.Domain;
public class Wallet : Entity
{
    public required string DisplayName { get; set; }
}
