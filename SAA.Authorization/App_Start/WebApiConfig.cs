using SAA.Authorization.Security;
using System.Web.Http;
using System.Web.Http.Dispatcher;

namespace SAA.Authorization
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            TokenInspector tokenInspector = new TokenInspector { InnerHandler = new HttpControllerDispatcher(config) };

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional },
                constraints: null,
                handler: tokenInspector
            );
        }
    }
}
