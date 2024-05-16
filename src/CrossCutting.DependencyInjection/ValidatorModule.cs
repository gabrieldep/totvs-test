using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Wallets.Application.Validators;

namespace CrossCutting.DependencyInjection;

internal static class ValidatorModule
{
    public static void AddValidatorModule(this IServiceCollection services)
    {
        services.AddTransient<IValidator<WalletStocksPosition>, GetWalletStocksPositionValidator>();
    }
}