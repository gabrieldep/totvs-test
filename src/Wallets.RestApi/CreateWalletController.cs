using Core.RestApi.Filters;

using MediatR;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Wallets.Application;

namespace Wallets.RestApi;
[ApiController]
[Route("api/wallets")]
public class CreateWalletController(ISender sender) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SaveChanges]
    public async Task<IActionResult> PostAsync([FromBody] CreateWalletRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateWallet { DisplayName = request.DisplayName };
        var walletId = await sender.Send(command, cancellationToken);

        return Ok(walletId);
    }
}

public record CreateWalletRequest
{
    public string DisplayName { get; set; } = string.Empty;
}
