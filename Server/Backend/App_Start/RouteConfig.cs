using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace Backend.App_Start
{
    /// <summary>
    /// How urls are routed.
    /// </summary>
    public class RouteConfig
    {
        /// <summary>
        /// Register the routes.
        /// </summary>
        /// <param name="routes">The route object</param>
        public static void RegisterRoutes(HttpRouteCollection routes)
        {
            routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });
        }
    }
}