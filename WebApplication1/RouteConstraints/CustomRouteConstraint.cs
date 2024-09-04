namespace WebApplication1.RouteConstraints
{
    public class CustomRouteConstraint : IRouteConstraint
    {
        public bool Match(
            HttpContext? httpContext,
            IRouter? route,
            string routeKey,
            RouteValueDictionary values,
            RouteDirection routeDirection)
        {
            ;
            return true;
        }
    }
}
