using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PokerStatBoard
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "PlayerStats",
                "PlayerStats/{name}",
                new { controller = "PlayerStats", action = "Name" }
            );

            routes.MapRoute(
                "StartGame",
                "StartGame/{groupName}",
                new { controller = "StartGame", action = "GroupName" }
            );

            routes.MapRoute(
                "EndGame",
                "EndGame/{groupName}",
                new { controller = "EndGame", action = "GroupName" }
            );

            routes.MapRoute(
                "Game",
                "Game/{groupName}",
                new { controller = "Game", action = "GroupName" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
