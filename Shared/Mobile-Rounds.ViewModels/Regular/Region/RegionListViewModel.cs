// <copyright file="RegionListViewModel.cs" company="SolarWorld Capstone Team">
// Copyright (c) SolarWorld Capstone Team. All rights reserved.
// </copyright>

using Mobile_Rounds.ViewModels.Shared;
using Mobile_Rounds.ViewModels.Models;
using Mobile_Rounds.ViewModels.Admin;
using System.Collections.ObjectModel;
using Newtonsoft.Json;


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

        public RegionListViewModel()
        {
            this.Regions = new ObservableCollection<RegionModelSource>();
            //this.Regions.Add(new RegionModelSource() { Name = "North Region" });
            //this.Regions.Add(new RegionModelSource() { Name = "South Region" });


            //gotta figure out how to pull the region names out and do the thing with new RegionModelSource()
            Newtonsoft.Json.JsonConvert.DeserializeObject<RegionModel>(File.ReadAllText(@"ms-appx:///region_test.json"));
            //i assume the deserialization will nicely append to this.Regions, but i don't know because errors.
            //i'd love to figure out wth is up with the error on File.ReadAllText, because newtonsoft's documentation sport's that same thing
        }

        private RegionModelSource region;

    }
}
