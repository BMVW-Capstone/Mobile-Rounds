// <copyright file="HomePageViewModel.cs" company="SolarWorld Capstone Team">
// Copyright (c) SolarWorld Capstone Team. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Mobile_Rounds.ViewModels.Shared.Commands;
using Mobile_Rounds.ViewModels.Platform;
using Mobile_Rounds.ViewModels.Models;
using Newtonsoft.Json;
using Mobile_Rounds.ViewModels.Shared.DbModels;

namespace Mobile_Rounds.ViewModels.Shared.Home
{
    /// <summary>
    /// Represents the bsaic data for the home screens as exposed to XAML.
    /// </summary>
    public class HomePageViewModel : BaseViewModel
    {
        /// <summary>
        /// Gets the property used to handle the start of rounds.
        /// </summary>
        public ICommand StartRound { get; private set; }

        /// <summary>
        /// Gets the property used to handle the syncing of data.
        /// </summary>
        public AsyncCommand Sync { get; private set; }


        public bool IsSyncing
        {
            get
            {
                return this.isSyncing;
            }

            set
            {
                this.isSyncing = value;
                this.RaisePropertyChanged(nameof(this.IsSyncing));
                this.RaisePropertyChanged(nameof(this.CanProgress));
                this.Sync.RaiseExecuteChanged();
            }
        }

        public bool CanProgress { get { return !this.IsSyncing; } }


        /// <summary>
        /// Initializes a new instance of the <see cref="HomePageViewModel"/> class.
        /// Creates and sets defaults for the view model.
        /// </summary>
        public HomePageViewModel()
        {
#if DEBUG
            // only make us admin if debugging.
            this.IsAdmin = true;
#endif
            this.GoHome = null;

            const string Host = "http://localhost:1797";

            this.Sync = new AsyncCommand(async (obj) =>
            {
                this.IsSyncing = true;
                IApiRequest request = ServiceResolver.Resolve<IApiRequest>();
                var handler = Platform.ServiceResolver.Resolve<IFileHandler>();

                var regions = await request.GetAsync<List<RegionModel>>($"{Host}/api/regions");
                var regionResult = new RegionHandler() { Regions = regions };
                await handler.SaveFileAsync("regions.json", regionResult);

                var stations = await request.GetAsync<List<StationModel>>($"{Host}/api/stations");
                var stationResult = new StationHandler() { Stations = stations };
                await handler.SaveFileAsync("stations.json", stationResult);

                var items = await request.GetAsync<List<ItemModel>>($"{Host}/api/items");
                var itemResult = new ItemHandler() { Items = items };
                await handler.SaveFileAsync("items.json", itemResult);

                var units = await request.GetAsync<List<UnitOfMeasureModel>>($"{Host}/api/units");
                var unitResult = new UnitHandler() { Units = units };
                await handler.SaveFileAsync("units.json", unitResult);

                this.IsSyncing = false;
            }, this.CanSync);

            this.StartRound = new StartRoundCommand();
            
        }

        private bool CanSync(object sender)
        {
            return !this.IsSyncing;
        }

        private bool isSyncing;
    }
}
