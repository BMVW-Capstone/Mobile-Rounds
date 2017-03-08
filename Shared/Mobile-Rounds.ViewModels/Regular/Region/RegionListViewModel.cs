// <copyright file="RegionListViewModel.cs" company="SolarWorld Capstone Team">
// Copyright (c) SolarWorld Capstone Team. All rights reserved.
// </copyright>

using Mobile_Rounds.ViewModels.Shared;
using Mobile_Rounds.ViewModels.Models;
using Mobile_Rounds.ViewModels.Admin;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System;
using Windows.Storage;
using Windows.Foundation;
using Mobile_Rounds.ViewModels.Platform;
using System.Collections.Generic;
using Mobile_Rounds.ViewModels.Shared.DbModels;
using Mobile_Rounds.ViewModels.Regular.StartRounds;

namespace Mobile_Rounds.ViewModels.Regular.Region
{
    public class RegionListViewModel : BaseViewModel
    {
        public ObservableCollection<RegionModelSource> Regions { get; set; }

        public RegionModelSource Selected
        {
            get
            {
                return this.region;
            }

            set
            {
                this.region = value;
                if (this.region != null && this.region.Navigate != null)
                {

                    this.region.Navigate.Execute(this);
                }

                this.RaisePropertyChanged(nameof(this.Selected));
            }
        }

        
        public RegionListViewModel(StartRoundViewModel parent, string reads)
        {
            this.Regions = new ObservableCollection<RegionModelSource>();
            var result = JsonConvert.DeserializeObject<RegionHandler>(reads);
            this.Crumbs.Add(new Shared.Controls.BreadcrumbItemModel("Regions", parent.Navigate));

            foreach (var region in result.Regions)
            {
                this.Regions.Add(new RegionModelSource(parent.Navigate)
                {
                    Id = region.Id,
                    Name = region.Name,
                    Stations = region.Stations
                });
            }
        }

        private RegionModelSource region;

    }
}
