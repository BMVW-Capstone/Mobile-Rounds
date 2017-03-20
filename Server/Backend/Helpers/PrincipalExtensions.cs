using Mobile_Rounds.ViewModels.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace Backend.Helpers
{
    public static class PrincipalExtensions
    {
        /// <summary>
        /// Determines if the user who made a request is in the app admin group or not.
        /// </summary>
        /// <param name="user">The user to check.</param>
        /// <param name="config">The settings to use when checking.</param>
        /// <returns><code>True</code> if they are in the admin group, otherwise <code>False</code>.</returns>
        public static bool IsAppAdmin(this IPrincipal user, ISettings config)
        {
            return user.IsInRole(config.GetValue<string>(Constants.Web.AdminGroupKey));
        }
    }
}