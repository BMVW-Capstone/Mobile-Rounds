// <copyright file="RegionListViewModel.cs" company="SolarWorld Capstone Team">
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

namespace Mobile_Rounds.ViewModels.Regular.Region
{
    public class RegionListViewModel : BaseViewModel
    {
        public ObservableCollection<RegionModel> Regions { get; set; }

        public RegionModel Selected
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
            this.Regions = new ObservableCollection<RegionModel>();
        }

        private RegionModel region;

    }
}
