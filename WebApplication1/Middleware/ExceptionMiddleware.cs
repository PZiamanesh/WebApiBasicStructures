
namespace WebApplication1.Middleware
{
    public class ExceptionMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
			try
			{
                await next.Invoke(context);
            }
			catch
			{
				throw;
			}
        }
    }

	public static class ExceptionMiddlewareExtension
	{
		public static IApplicationBuilder ExceptionMiddleware(this IApplicationBuilder app)
		{
			return app.UseMiddleware<ExceptionMiddleware>();
		}
	}
}
