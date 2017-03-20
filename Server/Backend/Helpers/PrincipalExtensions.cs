// <copyright file="PrincipalExtensions.cs" company="SolarWorld Capstone Team">
// Copyright (c) SolarWorld Capstone Team. All rights reserved.
// </copyright>

using System.Security.Principal;
using Mobile_Rounds.ViewModels.Platform;

namespace Backend.Helpers
{
    /// <summary>
    /// Provides a static helper method for accessing the users rights.
    /// </summary>
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