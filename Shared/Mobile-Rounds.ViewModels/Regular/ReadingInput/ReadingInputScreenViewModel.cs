// <copyright file="ReadingInputScreenViewModel.cs" company="SolarWorld Capstone Team">
// Copyright (c) SolarWorld Capstone Team. All rights reserved.
// </copyright>

using Mobile_Rounds.ViewModels.Admin.Items;
using Mobile_Rounds.ViewModels.Models;
using Mobile_Rounds.ViewModels.Regular.Region;
using Mobile_Rounds.ViewModels.Shared;
using Mobile_Rounds.ViewModels.Shared.Commands;
using Mobile_Rounds.ViewModels.Shared.Controls;
using Mobile_Rounds.ViewModels.Shared.DbModels;
using Newtonsoft.Json;
using System;
using System.Linq;

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
        public ReadingInputScreenViewModel(RegionModelSource region, StationModel station, string reads)
        {
            var regionNav = new AsyncCommand((obj) =>
            {
                Navigator.Navigate(Shared.Navigation.NavigationType.StationSelect);
            });
            this.Crumbs.Add(new BreadcrumbItemModel(region.Name, region.Navigate));
            this.Crumbs.Add(new BreadcrumbItemModel(station.Name, station.Navigate));
            this.Input = new ReadingInputViewModel();
            this.ListModel = new ReadingInputListViewModel(this);
            var result = JsonConvert.DeserializeObject<ItemHandler>(reads);
            foreach (var item in result.Items)
            {
                if (item.StationId == station.Id)
                {
                    var newMeter = new Meter(item.Meter)
                    {
                        Name = item.Name,
                        MeterName = item.Meter
                    };


                    newMeter.LastReading = new ReadingInput();
                    newMeter.TwoReadingsAgo = new ReadingInput();
                    newMeter.ThreeReadingsAgo = new ReadingInput();
                    newMeter.FourReadingsAgo = new ReadingInput();

                    var compType = ComparisonTypeViewModel.Locate(item.Specification.ComparisonType);
                    
                    newMeter.TodaysReading = new ReadingInput()
                    {
                        MinimumValue = item.Specification.LowerBound,
                        MaximumValue = item.Specification.UpperBound,
                        UnitAbbreviation = item.Specification.UnitOfMeasure.Abbreviation,
                        ValueBounds = compType.AsEnum(),
                    };

                    var count = item.PastFourReadings.Count();

                    if(count > 0)
                    {
                        newMeter.LastReading.StringValue = item.PastFourReadings.ElementAt(0).Value;
                        newMeter.LastReading.IsWithinSpec = !item.PastFourReadings.ElementAt(0).IsOutOfSpec;
                        newMeter.LastReading.Notes = item.PastFourReadings.ElementAt(0).Comments;
                    }

                    if (count > 1)
                    {
                        newMeter.TwoReadingsAgo.StringValue = item.PastFourReadings.ElementAt(1).Value;
                        newMeter.TwoReadingsAgo.IsWithinSpec = !item.PastFourReadings.ElementAt(1).IsOutOfSpec;
                    }

                    if (count > 2)
                    {
                        newMeter.ThreeReadingsAgo.StringValue = item.PastFourReadings.ElementAt(2).Value;
                        newMeter.ThreeReadingsAgo.IsWithinSpec = !item.PastFourReadings.ElementAt(2).IsOutOfSpec;
                    }

                    if (count > 3)
                    {
                        newMeter.FourReadingsAgo.StringValue = item.PastFourReadings.ElementAt(3).Value;
                        newMeter.FourReadingsAgo.IsWithinSpec = !item.PastFourReadings.ElementAt(3).IsOutOfSpec;
                    }

                    this.ListModel.Meters.Add(newMeter);
                }
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
