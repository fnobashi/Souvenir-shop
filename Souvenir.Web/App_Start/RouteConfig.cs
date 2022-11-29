using System.Web.Mvc;
using System.Web.Routing;

namespace Souvenir.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            
            routes.MapMvcAttributeRoutes(); 

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "MainPage", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "Souvenir.Web", "Souvenir.Web.Areas.Admin" }
            );
        }
    }
}
