using Bonds.Application;

using Core.RestApi.Filters;

using MediatR;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bonds.RestApi;

[ApiController]
[Route("api/wallets/{walletId:guid}/invest/bond")]
public class CreateBondController(ISender sender) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SaveChanges]
    public async Task<IActionResult> PostAsync([FromRoute] Guid walletId, [FromBody] CreateBondRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateBond
        {
            WalletId = walletId,
            Description = request.Description,
            Value = request.Value,
            Expiration = request.Expiration,
            AnnualInterestRate = request.AnnualInterestRate
        };
        var bondId = await sender.Send(command, cancellationToken);
        return Ok(bondId);
    }
}

public record CreateBondRequest
{
    public string Description { get; set; } = string.Empty;
    public decimal Value { get; set; }
    public DateTime Expiration { get; set; }
    public decimal AnnualInterestRate { get; set; }
}
