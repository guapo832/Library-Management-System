using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace LibrarySystem
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Inventory",
                url: "{area}/{controller}/{action}/{id}",
                defaults: new {area="Inventory", controller = "Inventory", action = "Member", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Members",
                url: "{area}/{controller}/{action}/{id}",
                defaults: new { area = "Members", controller = "Member", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}