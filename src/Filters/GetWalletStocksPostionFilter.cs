using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using Wallets.Application;

public class GetWalletStocksPostionFilter : IAsyncActionFilter
{
    private readonly IValidatorFactory _validatorFactory;

    public GetWalletStocksPostionFilter(IValidatorFactory validatorFactory)
    {
        _validatorFactory = validatorFactory;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var actionArguments = context.ActionArguments;

        foreach (var argument in actionArguments)
        {
            var validator = _validatorFactory.GetValidator(argument.Value.GetType());

            if (validator != null)
            {
                var validationResult = await validator.ValidateAsync(argument.Value);

                if (!validationResult.IsValid)
                {
                    context.Result = new BadRequestObjectResult(validationResult.Errors);
                    return;
                }
            }
        }

        await next();
    }
}