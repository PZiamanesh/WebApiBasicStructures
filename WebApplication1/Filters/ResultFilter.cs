using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApplication1.Filters
{
    public class ResultFilter : IAsyncResultFilter
    {
        public async Task OnResultExecutionAsync(
            ResultExecutingContext context,
            ResultExecutionDelegate next)
        {
            ;

            await next();

            ;
        }
    }
}
