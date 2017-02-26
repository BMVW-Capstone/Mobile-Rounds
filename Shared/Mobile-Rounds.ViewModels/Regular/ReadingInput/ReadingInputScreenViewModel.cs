// <copyright file="ReadingInputScreenViewModel.cs" company="SolarWorld Capstone Team">
// Copyright (c) SolarWorld Capstone Team. All rights reserved.
// </copyright>

using Mobile_Rounds.ViewModels.Shared;
using Mobile_Rounds.ViewModels.Shared.Commands;
using Mobile_Rounds.ViewModels.Shared.Controls;
using Mobile_Rounds.ViewModels.Shared.DbModels;
using Newtonsoft.Json;

namespace Mobile_Rounds.ViewModels.Regular.ReadingInput
{
    /// <summary>
    /// Represents the data operations for the reading input screen.
    /// </summary>
    public class ReadingInputScreenViewModel : BaseViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReadingInputScreenViewModel"/> class.
        /// Represents the full view model for the page.
        /// </summary>
        public ReadingInputScreenViewModel(string regionName, string stationName, string reads)
        {
            var regionNav = new AsyncCommand((obj) =>
            {
                Navigator.Navigate(Shared.Navigation.NavigationType.StationSelect);
            });
            this.Crumbs.Add(new BreadcrumbItemModel(regionName, regionNav));
            this.Crumbs.Add(new BreadcrumbItemModel(stationName));
            this.Input = new ReadingInputViewModel();
            this.ListModel = new ReadingInputListViewModel(this);
            var result = JsonConvert.DeserializeObject<ItemHandler>(reads);
            foreach (var item in result.Items)
            {
                var newMeter = new Meter(item.Meter) { Name = item.Name };
                newMeter.YesterdaysReading = new ReadingInput();
                newMeter.TodaysReading = new ReadingInput();
                this.ListModel.Meters.Add(newMeter);
            }
        }

        /// <summary>
        /// Gets the model used for databinding the list part of the view.
        /// </summary>
        public ReadingInputListViewModel ListModel { get; private set; }

        /// <summary>
        /// Gets or sets the model for the input part of the screen.
        /// </summary>
        public ReadingInputViewModel Input
        {
            get
            {
                return this.input;
            }

            set
            {
                this.input = value;
                this.RaisePropertyChanged(nameof(this.Input));
            }
        }

        private ReadingInputViewModel input;
    }
}
