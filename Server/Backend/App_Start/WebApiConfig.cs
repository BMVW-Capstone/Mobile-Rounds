// <copyright file="WebApiConfig.cs" company="SolarWorld Capstone Team">
// Copyright (c) SolarWorld Capstone Team. All rights reserved.
// </copyright>

using System.Web.Http;
using Backend.App_Start;

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

            RouteConfig.RegisterRoutes(config.Routes);
        }
    }
}