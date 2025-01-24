using System;
using System.Net;
using Calc.ExpressionProcessor;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Calc.ActionFilters
{
    public class ExceptionFilter: IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context) { }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception == null)
                return;
            
            if (context.Exception is not ParseException argumentException )
                context.Result = new ObjectResult("Internal Server Error.")
                {
                    StatusCode = (int?)HttpStatusCode.InternalServerError
                };
            else
                context.Result = new ObjectResult(argumentException.Message)
                {
                    StatusCode = (int?)HttpStatusCode.BadRequest
                };

            context.ExceptionHandled = true;
        }
    }
}