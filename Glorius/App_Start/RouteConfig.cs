using Glorius.App_Start;
using Glorius.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Glorius
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapMvcAttributeRoutes();
            routes.LowercaseUrls = true;

            routes.MapRoute(
                "Root",
                "{action}",
                new { controller = "Home", action = "Products", id = UrlParameter.Optional },
                new { isMethodInHomeController = new RootRouteConstraint<HomeController>() }
            );
            routes.MapRoute(
               "RootCart",
               "{action}",
               new { controller = "Cart", action = "Cart", id = UrlParameter.Optional },
               new { isMethodInHomeController = new RootRouteConstraint<CartController>() }
           );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Products", id = UrlParameter.Optional }
            );
        }
    }
}
