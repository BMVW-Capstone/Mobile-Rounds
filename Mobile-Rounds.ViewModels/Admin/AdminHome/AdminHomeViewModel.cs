// <copyright file="AdminHomeViewModel.cs" company="SolarWorld Capstone Team">
// Copyright (c) SolarWorld Capstone Team. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Mobile_Rounds.ViewModels.Shared;
using Mobile_Rounds.ViewModels.Shared.Commands;

namespace Mobile_Rounds.ViewModels.Admin.AdminHome
{
    /// <summary>
    /// Represents the button actions the admin can use from
    /// the main admin page.
    /// </summary>
    public class AdminHomeViewModel : BaseViewModel
    {
        /// <summary>
        /// Gets the action to call to navigate to the Unit of Measurments screen.
        /// </summary>
        public ICommand GoToUnits { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminHomeViewModel"/> class.
        /// This also sets up all actions.
        /// </summary>
        public AdminHomeViewModel()
        {
            this.Crumbs.Add(new Shared.Controls.BreadcrumbItemModel("Admin"));
            this.GoToUnits = new AsyncCommand((obj) =>
            {
                Navigator.Navigate(Shared.Navigation.NavigationType.UnitOfMeasure);
            });
        }
    }
}
