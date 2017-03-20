﻿using Backend.Helpers;
using Mobile_Rounds.ViewModels.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace Backend.Attributes
{
    /// <summary>
    /// Provides a way to restrict users who are not admins.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
    public class AuthorizeAdmin : AuthorizeAttribute
    {
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            var authed = base.IsAuthorized(actionContext);
            if (!authed)
            {
                return authed;
            }

            // now verify against the user specified admin group
            var settings = new Settings();
            return actionContext.RequestContext.Principal.IsAppAdmin(settings);
        }
    }
}