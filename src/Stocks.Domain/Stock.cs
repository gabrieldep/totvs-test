using Investiments.Domain;

namespace Stocks.Domain;
public class Stock : Investiment
{
    public override InvestimentType Type { get; set; } = InvestimentType.Stock;
    public required string Code { get; set; }
    public int Amount { get; set; }
}
