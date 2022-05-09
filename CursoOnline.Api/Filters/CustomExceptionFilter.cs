using CursoOnline.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CursoOnline.Api.Filters
{
    public class CustomExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            context.HttpContext.Response.StatusCode = context.Exception is DomainException ? 502 : 500;
            context.Result = context.Exception is DomainException domainEx ?
                new JsonResult(domainEx.Errors) : new JsonResult(context.Exception.Message);

            base.OnException(context);
        }
    }
}
