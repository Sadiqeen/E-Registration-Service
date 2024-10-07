using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace VerifyService.Exceptions
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            Exception exception = context.Exception;

            if (exception.GetType().BaseType == typeof(HttpException))
            {
                HttpException httpException = (HttpException)context.Exception;
                int statusCode = httpException.StatusCode;

                context.Result = new ObjectResult(new
                {
                    error = context.Exception.Message,
                    stackTrace = context.Exception.StackTrace
                })
                {
                    StatusCode = statusCode
                };
            }
        }
    }
}