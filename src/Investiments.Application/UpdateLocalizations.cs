using Investiments.Application.Dtos;

using MediatR;

namespace Investiments.Application;
public record UpdateLocalizations : IRequest<Unit>
{
    public Guid InvestimentId { get; set; }
    public IEnumerable<InvestimentLocalizationData> Localizations { get; set; } = Enumerable.Empty<InvestimentLocalizationData>();
}

public class UpdateLocalizationsHandler : IRequestHandler<UpdateLocalizations, Unit>
{
    public Task<Unit> Handle(UpdateLocalizations request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
