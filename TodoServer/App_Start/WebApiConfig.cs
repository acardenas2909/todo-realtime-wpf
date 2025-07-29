using System.Web.Http;

namespace TodoServer
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Ruta de la API
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
