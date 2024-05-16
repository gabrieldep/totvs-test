using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Core.RestApi.Filters;
public class SaveChangesAttribute() : ActionFilterAttribute
{
    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var uow = context.HttpContext.RequestServices.GetRequiredService<DbContext>();
        await next();
        await uow.SaveChangesAsync(context.HttpContext.RequestAborted);
    }
}
