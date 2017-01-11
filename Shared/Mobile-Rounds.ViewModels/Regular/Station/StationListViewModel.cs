// <copyright file="StationListViewModel.cs" company="SolarWorld Capstone Team">
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
using System.Collections.ObjectModel;

namespace Mobile_Rounds.ViewModels.Regular.Station
{
    public class StationListViewModel : BaseViewModel
    {
        public ObservableCollection<StationModel> Stations { get; set; }

        public StationModel Selected
        {
            get
            {
                return this.station;
            }

            set
            {
                this.station = value;
                if (this.station != null && this.station.Navigate != null)
                {
                    this.station.Navigate.Execute(this);
                }

                this.RaisePropertyChanged(nameof(this.Selected));
            }
        }

        public StationListViewModel()
        {
            this.Crumbs.Add(new Shared.Controls.BreadcrumbItemModel("North Region"));

            this.Stations = new ObservableCollection<StationModel>();
            this.Stations.Add(new StationModel() { Name = "Compressor Room" });
            this.Stations.Add(new StationModel() { Name = "Air Vents" });
            this.Stations.Add(new StationModel() { Name = "Cooling Tanks" });
        }

        private StationModel station;
    }
}
