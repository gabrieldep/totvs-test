using Core.RestApi.Filters;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wallets.Application;

namespace Wallets.RestApi;

[ApiController]
[Route("api/wallets")]
public class GetWalletStocksPostionController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IValidator<GetWalletStocksPosition> _validator;

    public GetWalletStocksPostionController(ISender sender, IValidator<GetWalletStocksPosition> validator)
    {
        _sender = sender;
        _validator = validator;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [SaveChanges]
    public async Task<IActionResult> GetAsync(Guid walletId, CancellationToken cancellationToken)
    {
        var command = new GetWalletStocksPosition { WalletId = walletId };

        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var stockPosition = await _sender.Send(command, cancellationToken);

        return Ok(stockPosition);
    }
}