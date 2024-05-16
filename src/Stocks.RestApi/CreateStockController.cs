using Core.RestApi.Filters;

using MediatR;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Stocks.Application;

namespace Stocks.RestApi;

[ApiController]
[Route("api/wallets/{walletId:guid}/invest/stock")]
public class CreateStockController(ISender sender) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SaveChanges]
    public async Task<IActionResult> PostAsync([FromRoute] Guid walletId, [FromBody] CreateStockRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateStock
        {
            WalletId = walletId,
            Description = request.Description,
            Value = request.Value,
            Code = request.Code,
            Amount = request.Amount
        };
        var bondId = await sender.Send(command, cancellationToken);
        return Ok(bondId);
    }
}

public record CreateStockRequest
{
    public string Description { get; set; } = string.Empty;
    public decimal Value { get; set; }
    public string Code { get; set; } = string.Empty;
    public int Amount { get; set; }
}
