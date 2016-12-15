// <copyright file="GoHomeCommand.cs" company="SolarWorld Capstone Team">
// Copyright (c) SolarWorld Capstone Team. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Mobile_Rounds.ViewModels.Shared.Commands
{
    /// <summary>
    /// Represents the action to call when navigating to the home page.
    /// </summary>
    public class GoHomeCommand : AsyncCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GoHomeCommand"/> class.
        /// </summary>
        public GoHomeCommand()
        {
            this.Action = (obj) =>
            {
                BaseViewModel.Navigator.Navigate(Navigation.NavigationType.Home);
            };
            this.Condition = (obj) => { return true; };
        }
    }
}
