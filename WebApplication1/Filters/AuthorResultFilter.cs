using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApplication1.ViewModels;

namespace WebApplication1.Filters
{
    public class AuthorResultFilter : IAsyncResultFilter
    {
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var actionResult = context.Result as ObjectResult;

            if (actionResult?.Value == null ||
                actionResult.StatusCode < 200 ||
                actionResult.StatusCode >= 300)
            {
                await next();
                return;
            }

            var mapper = context.HttpContext.RequestServices.GetRequiredService<IMapper>();
            var authorResult = mapper.Map<AuthorResult>(actionResult.Value);

            await next();
        }
    }
}
