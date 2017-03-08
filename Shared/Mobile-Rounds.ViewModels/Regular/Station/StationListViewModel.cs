// <copyright file="StationListViewModel.cs" company="SolarWorld Capstone Team">
// Copyright (c) SolarWorld Capstone Team. All rights reserved.
// </copyright>

using Mobile_Rounds.ViewModels.Shared;
using System.Collections.ObjectModel;
using Mobile_Rounds.ViewModels.Models;
using Newtonsoft.Json;
using Mobile_Rounds.ViewModels.Shared.DbModels;
using System;
using Mobile_Rounds.ViewModels.Regular.Region;

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

        public StationListViewModel(string reads, RegionModelSource region)
        {
            this.Crumbs.Add(new Shared.Controls.BreadcrumbItemModel(region.Name, region.Navigate));
            this.Stations = new ObservableCollection<StationModel>();
            var result = JsonConvert.DeserializeObject<StationHandler>(reads);
            foreach (var station in result.Stations)
            {
                if (station.RegionId == region.Id)
                {
                    this.Stations.Add(new StationModel(region)
                    {
                        Id = station.Id,
                        Name = station.Name
                    });
                }
            }
        }

        private StationModel station;
    }
}
