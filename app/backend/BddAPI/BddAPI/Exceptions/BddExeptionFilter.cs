using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BddAPI.Exceptions;

public class BddExeptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is not BddCustomException netLinkCustomException) return;
        context.Result = context.Exception switch
        {
            UserException or ReservationException or CommunityCenterException => new BadRequestObjectResult(new
            {
                netLinkCustomException.Message
            }),
            NotFoundException => new NotFoundObjectResult(new { netLinkCustomException.Message }),
            UnauthorizedException => new UnauthorizedObjectResult(new { netLinkCustomException.Message }),
            _ => context.Result
        };

        context.ExceptionHandled = true;
    }
}