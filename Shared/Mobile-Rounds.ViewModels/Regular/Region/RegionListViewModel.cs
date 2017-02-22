// <copyright file="RegionListViewModel.cs" company="SolarWorld Capstone Team">
// Copyright (c) SolarWorld Capstone Team. All rights reserved.
// </copyright>

using Mobile_Rounds.ViewModels.Shared;
using System.Collections.ObjectModel;

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
            this.Regions.Add(new RegionModelSource() { Name = "North Region" });
            this.Regions.Add(new RegionModelSource() { Name = "South Region" });
        }

        private RegionModelSource region;

    }
}
