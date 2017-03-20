// <copyright file="Settings.cs" company="SolarWorld Capstone Team">
// Copyright (c) SolarWorld Capstone Team. All rights reserved.
// </copyright>

using System;
using System.Configuration;
using Mobile_Rounds.ViewModels.Platform;

namespace Backend.Helpers
{
    /// <summary>
    /// A wrapper around the ConfigurationManager.AppSettings.
    /// </summary>
    public class Settings : ISettings
    {
        /// <inheritdoc/>
        public TReturn GetValue<TReturn>(string key)
        {
            return (TReturn)(ConfigurationManager.AppSettings[key] as object);
        }

        /// <inheritdoc/>
        public void SaveValue(string key, object value)
        {
            throw new NotImplementedException();
        }
    }
}