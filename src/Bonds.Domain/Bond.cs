using Investiments.Domain;

namespace Bonds.Domain;

public class Bond : Investiment
{
    public override InvestimentType Type { get; set; } = InvestimentType.Bond;
    public DateTime Expiration { get; set; }
    public decimal AnnualInterestRate { get; set; }
}
