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
using Mobile_Rounds.ViewModels.Shared.Navigation;
using System.Diagnostics;
using Mobile_Rounds.ViewModels.Platform;
using Mobile_Rounds.ViewModels.Models;
using Mobile_Rounds.ViewModels.Shared.DbModels;
using Mobile_Rounds.ViewModels.Admin.Regions;

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
        /// Gets the action to call to navigate to the Stations screen
        /// </summary>
        public ICommand GoToStations { get; private set; }

        /// <summary>
        /// Gets the action to call to navigate to the Items screen
        /// </summary>
        public ICommand GoToItems { get; private set; }

        /// <summary>
        /// Gets the action to call to navigate to the Regions screen
        /// </summary>
        public ICommand GoToRegions { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminHomeViewModel"/> class.
        /// This also sets up all actions.
        /// </summary>
        public AdminHomeViewModel()
        {
            this.Crumbs.Add(new Shared.Controls.BreadcrumbItemModel("Admin"));

            this.GoToUnits = new AsyncCommand((obj) =>
            {
                Navigator.Navigate(NavigationType.UnitOfMeasure);
            });

            this.GoToStations = new AsyncCommand((obj) =>
            {
                Navigator.Navigate(NavigationType.AdminStations);
            });

            this.GoToRegions = new AsyncCommand((obj) =>
            {
                Navigator.Navigate(NavigationType.Regions);
            });

            this.GoToItems = new AsyncCommand(async (obj) =>
            {
                //TODO: This is a hack just for demonstration purposes.
                var fm = ServiceResolver.Resolve<IFileHandler>();

                var regions = await fm.GetFileAsync<RegionHandler>("regions.json");
                var stations = await fm.GetFileAsync<StationHandler>("stations.json");
                var items = await fm.GetFileAsync<ItemHandler>("items.json");
                var units = await fm.GetFileAsync<UnitHandler>("units.json");

                var east = regions.Regions[0];
                var nox = stations.Stations.First(r => r.RegionId == east.Id);

                var vm = new ViewModels.Admin.Items.ItemScreenViewModel(
                    east,
                    nox,
                    units.Units,
                    items.Items);

                Navigator.Navigate(NavigationType.AdminItems, vm);
            });
        }
    }
}
