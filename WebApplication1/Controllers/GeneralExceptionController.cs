using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class GeneralExceptionController : Controller
    {
        [Route("/error")]
        public IActionResult Error()
        {
            var exception = HttpContext.Features.Get<IExceptionHandlerFeature>();
            if (exception is { })
            {
                var errorMsg = exception.Error.Message ?? "nooooo";
                return Content($"<h1>{errorMsg}</h1>", "text/html");
            }

            return RedirectToAction("List", "Author");
        }
    }
}
