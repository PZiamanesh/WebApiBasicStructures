using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApplication1.Filters
{
    public class ResourceFilter : IAsyncResourceFilter
    {
        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            ;

            await next();

            ;
        }
    }
}
