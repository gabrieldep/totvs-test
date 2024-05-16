using Core.RestApi.Filters;

using Investiments.Application;
using Investiments.Application.Dtos;

using MediatR;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Investiments.RestApi;

[ApiController]
[Route("api/investiments/{investimentId:guid}/localizations")]
public class UpdateLocalizationsController(ISender sender) : ControllerBase
{
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SaveChanges]
    public async Task<IActionResult> PutAsync([FromRoute] Guid investimentId, [FromBody] IEnumerable<InvestimentLocalizationData> localizations, CancellationToken cancellationToken)
    {
        var command = new UpdateLocalizations
        {
            InvestimentId = investimentId,
            Localizations = localizations
        };
        await sender.Send(command, cancellationToken);
        return NoContent();
    }
}
