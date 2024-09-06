using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApplication1.Filters
{
    public class ActionFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(
            ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var problemDetails = new ValidationProblemDetails()
                {
                    Status = StatusCodes.Status422UnprocessableEntity,
                    Title = "Input Model Validation Error Occured.",
                    Errors = new Dictionary<string, string[]>()
                };

                foreach (var inputValue in context.ModelState.Values)
                {
                   
                }
            }
            await next();
        }
    }
}
