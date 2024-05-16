using Core.RestApi.Filters;

using MediatR;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Wallets.Application;

namespace Wallets.RestApi;
[ApiController]
[Route("api/wallets")]
public class GetWalletStocksPostionController(ISender sender) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [SaveChanges]
    public async Task<IActionResult> GetAsync(Guid walletId, CancellationToken cancellationToken)
    {
        var command = new GetWalletStocksPosition { WalletId = walletId };
        var stockPosition = await sender.Send(command, cancellationToken);

        return Ok(stockPosition);
    }
}