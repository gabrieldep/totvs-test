using Core.Domain;

namespace Investiments.Domain;
public class InvestimentLocalization : Entity
{
    public Guid InvestimentId { get; set; }
    public Investiment Investiment { get; set; } = null!;
    public string LanguageCode { get; set; } = null!;
    public string Description { get; set; } = null!;
}
