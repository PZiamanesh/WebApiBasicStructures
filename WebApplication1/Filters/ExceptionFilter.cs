using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApplication1.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            context.ExceptionHandled = true;
        }
    }
}
