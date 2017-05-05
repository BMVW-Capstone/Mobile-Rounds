// <copyright file="ReadingInputScreenViewModel.cs" company="SolarWorld Capstone Team">
// Copyright (c) SolarWorld Capstone Team. All rights reserved.
// </copyright>

using Mobile_Rounds.ViewModels.Admin.Items;
using Mobile_Rounds.ViewModels.Models;
using Mobile_Rounds.ViewModels.Platform;
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
        public ReadingInputScreenViewModel(RegionModelSource region, StationModel station)
        {
            var regionNav = new AsyncCommand((obj) =>
            {
                Navigator.Navigate(Shared.Navigation.NavigationType.StationSelect);
            });

            this.Save = new AsyncCommand((obj) => this.SaveInput(this.input), this.CanSave);

            this.station = station;
            this.Crumbs.Add(new BreadcrumbItemModel("Areas", region.NavigateRoot));
            this.Crumbs.Add(new BreadcrumbItemModel(region.Name, region.Navigate));
            this.Crumbs.Add(new BreadcrumbItemModel(station.Name, station.Navigate));
            this.Input = new ReadingInputViewModel(this.Save, null);
            this.ListModel = new ReadingInputListViewModel(this);
            foreach (var item in station.Items)
            {
                if (item.StationId == station.Id)
                {
                    var newMeter = new Meter(item.Meter, item)
                    {
                        Id = item.Id,
                        Name = item.Name,
                        MeterName = item.Meter
                    };
                    newMeter.LastReading = new ReadingInput();
                    newMeter.TwoReadingsAgo = new ReadingInput();
                    newMeter.ThreeReadingsAgo = new ReadingInput();
                    newMeter.FourReadingsAgo = new ReadingInput();
                    newMeter.ComparisonType = ComparisonTypeViewModel.Locate(item.Specification.ComparisonType);
                    newMeter.TodaysReading = new ReadingInput()
                    {
                        MinimumValue = item.Specification.LowerBound,
                        MaximumValue = item.Specification.UpperBound,
                        UnitAbbreviation = item.Specification.UnitOfMeasure.Abbreviation,
                        ValueBounds = newMeter.ComparisonType.AsEnum(),
                        IsWithinSpec = false
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
                if(this.input != null)
                {
                    //save the input prior to going to the next item.
                    this.SaveInput(value);
                }
                this.input = value;
                this.RaisePropertyChanged(nameof(this.Input));
            }
        }

        public AsyncCommand Save { get; private set; }

        private bool CanSave(object toSave)
        {
            if(this.input.Validate() == false)
            {
                //ensure we have a comment
                return !string.IsNullOrEmpty(this.input.Comments);
            }
            //valid input, so just allow the save.
            return true;
        }

        private async void SaveInput(ReadingInputViewModel newData)
        {
            if (this.input.ItemId == Guid.Empty) return;
            var io = ServiceResolver.Resolve<IFileHandler>();

            var existingReading = ReadingManager.Find(newData.ItemId);
            //reading already exists or was just created, so just update the values.
            var item = this.station.Items.FirstOrDefault(i => i.Id == newData.ItemId);

            if (newData.IsBooleanInput)
            {
                existingReading.Value = newData.BooleanValue.ToString();
            }
            else
            {
                existingReading.Value = newData.StringValue;
            }
            existingReading.Comments = newData.Comments;
            existingReading.TimeTaken = DateTime.UtcNow;

            // Now validate the users input
            var validator = ComparisonTypeViewModel.Locate(newData.InputType);
            if(validator.UsesOneInput)
            {
                existingReading.IsOutOfSpec = validator.Validate(
                    value: existingReading.Value,
                    max: item.Specification.UpperBound) == false;
            }
            else
            {
                existingReading.IsOutOfSpec = validator.Validate(
                    value: existingReading.Value,
                    min: item.Specification.LowerBound,
                    max: item.Specification.UpperBound) == false;
            }
            //finally, write out the result to file so that way it doesn't get lost.
            item.CurrentReading = existingReading;

            var model = this.ListModel.Meters.FirstOrDefault(m => m.Id == this.ListModel.Selected.Id);
            model.IsComplete = true;

            await ReadingManager.SaveReadingsToDiskAsync();
            //place binding for completed items here. probably.
        }

        private ReadingInputViewModel input;
        private StationModel station;
        private ItemModel item;
    }
}
