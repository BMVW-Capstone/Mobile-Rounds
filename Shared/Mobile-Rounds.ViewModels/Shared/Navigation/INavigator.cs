// <copyright file="INavigator.cs" company="SolarWorld Capstone Team">
// Copyright (c) SolarWorld Capstone Team. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile_Rounds.ViewModels.Shared.Navigation
{
    /// <summary>
    /// A service class that knows how to navigate between pages.
    /// </summary>
    public interface INavigator
    {
        /// <summary>
        /// Gets the last data passed to the Navigate method.
        /// </summary>
        /// <typeparam name="T">The type of object you expect it to have.</typeparam>
        /// <returns>The previously stored data.</returns>
        T GetNavigationData<T>();

        /// <summary>
        /// Navigates to the specific type of screen.
        /// </summary>
        /// <param name="type">The screen to go to.</param>
        void Navigate(NavigationType type);

        /// <summary>
        /// Navigates to the specific type of screen passing the given
        /// constructor object to the views constructor.
        /// </summary>
        /// <param name="type">The screen to go to.</param>
        /// <param name="constructorParam">The parameter to pass to the page constructor.</param>
        void Navigate(NavigationType type, object constructorParam);
    }
}
