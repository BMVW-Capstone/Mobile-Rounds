// <copyright file="WebApiApplication.asax.cs" company="SolarWorld Capstone Team">
// Copyright (c) SolarWorld Capstone Team. All rights reserved.
// </copyright>

using Newtonsoft.Json;
using System.Web.Http;

namespace Backend
{
    /// <summary>
    /// The main class for the starting the server.
    /// </summary>
    public class WebApiApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// Fired when the application is starting up for the first time.
        /// </summary>
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            this.ConfigureJSON();
        }

        private void ConfigureJSON()
        {
            // register the default settings for converting items to JSON
            // this is important because we spend a lot of bits just passing
            // null or empty values. This is ALL for performance purposes.
            HttpConfiguration config = GlobalConfiguration.Configuration;

            // ignore whitespace and just print as small as possible.
            config.Formatters.JsonFormatter.SerializerSettings.Formatting =
                Formatting.None;

            // ignore missing properties/set them to null or empty.
            config.Formatters
                .JsonFormatter
                .SerializerSettings
                .MissingMemberHandling = MissingMemberHandling.Ignore;

            // ignore null values.
            config.Formatters
                .JsonFormatter
                .SerializerSettings
                .NullValueHandling = NullValueHandling.Ignore;
        }
    }
}
