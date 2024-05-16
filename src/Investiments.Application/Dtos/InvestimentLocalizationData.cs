namespace Investiments.Application.Dtos;
public record InvestimentLocalizationData
{
    public string LanguageCode { get; set; } = null!;
    public string Description { get; set; } = null!;
}
