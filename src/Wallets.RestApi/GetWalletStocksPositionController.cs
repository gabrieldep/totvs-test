using Core.RestApi.Filters;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wallets.Application;
using Wallets.Application.Validators;

namespace Wallets.RestApi;

[ApiController]
[Route("api/wallet/{walletId}/stocks-position")]
public class GetWalletStocksPositionController(ISender sender, IValidator<WalletStocksPosition> validator) : ControllerBase
{

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [SaveChanges]
    public async Task<IActionResult> GetAsync([FromRoute] Guid walletId, CancellationToken cancellationToken)
    {
        var walletStocksPosition = new WalletStocksPosition { WalletId = walletId };

        var validationResult = await validator.ValidateAsync(walletStocksPosition, cancellationToken);
        if (!validationResult.IsValid)
            return NotFound(validationResult.Errors);

        var command = new GetWalletStocksPosition() { WalletId = walletId };
        var stockPosition = await sender.Send(command, cancellationToken);

        return Ok(stockPosition);
    }
}