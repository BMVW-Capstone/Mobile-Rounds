// <copyright file="AuthorizeAdminAttribute.cs" company="SolarWorld Capstone Team">
// Copyright (c) SolarWorld Capstone Team. All rights reserved.
// </copyright>

using System;
using System.Web.Http;
using System.Web.Http.Controllers;
using Backend.Helpers;
using Mobile_Rounds.ViewModels.Platform;

namespace Backend.Attributes
{
    /// <summary>
    /// Provides a way to restrict users who are not admins.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
    public class AuthorizeAdminAttribute : AuthorizeAttribute
    {
        /// <inheritdoc/>
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            var authed = base.IsAuthorized(actionContext);
            if (!authed)
            {
                return authed;
            }
            var settings = new Settings();

            if (settings.GetValue<string>(Constants.Web.ModeKey) != "development")
            {
                // now verify against the user specified admin group
                return actionContext.RequestContext.Principal.IsAppAdmin(settings);
            }

            return true;
        }
    }
}