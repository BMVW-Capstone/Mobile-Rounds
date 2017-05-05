// <copyright file="GoHomeCommand.cs" company="SolarWorld Capstone Team">
// Copyright (c) SolarWorld Capstone Team. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

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
                if (RoundManager.CurrentRound == null)
                {
                    BaseViewModel.Navigator.Navigate(Navigation.NavigationType.Home);
                }
                else
                {
                    BaseViewModel.Navigator.Navigate(Navigation.NavigationType.PauseScreen);
                }
            };
            this.Condition = (obj) => { return true; };
        }
    }
}
