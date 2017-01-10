// <copyright file="WebApiConfig.cs" company="SolarWorld Capstone Team">
// Copyright (c) SolarWorld Capstone Team. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Backend
{
    /// <summary>
    /// Provides the basic configuration for the web server.
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// Registers all parts of the server for use.
        /// </summary>
        /// <param name="config">The current http configuration to bootstrap.</param>
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });
        }
    }
}
