// <copyright file="ReadingInputListViewModel.cs" company="SolarWorld Capstone Team">
// Copyright (c) SolarWorld Capstone Team. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mobile_Rounds.ViewModels.Shared;

namespace Mobile_Rounds.ViewModels.Regular.ReadingInput
{
    /// <summary>
    /// Represents the model backing the list on the left of the new readings screen.
    /// </summary>
    public class ReadingInputListViewModel : BaseViewModel
    {
        /// <summary>
        /// Gets or sets the list of units that are displayed to the user.
        /// </summary>
        public ObservableCollection<Meter> Meters { get; set; }

        /// <summary>
        /// Gets or sets the currently selected unit in the list.
        /// If set to null, it clears out the selection.
        /// </summary>
        public Meter Selected
        {
            get
            {
                return this.selectedMeter;
            }

            set
            {
                this.selectedMeter = value;
                if (this.selectedMeter != null)
                {
                    ReadingInput today = this.selectedMeter.TodaysReading;

                    var newInput = new ReadingInputViewModel(today, this.selectedMeter.Item, this.parent.Save, null);
                    newInput.ItemId = this.selectedMeter.Id;

                    // here we get the last known reading, and set the value for the user to see.
                    var currentReading = ReadingManager.Find(newInput.ItemId);
                    newInput.StringValue = currentReading?.Value;

                    // just try to set the boolean value, that way we can skip actually checking the
                    //type of value it should be.
                    bool output = false;
                    if (bool.TryParse(currentReading?.Value, out output))
                    {
                        newInput.BooleanValue = output;
                    }
                    newInput.IsInSpec = currentReading?.IsOutOfSpec == false;
                    newInput.Comments = currentReading?.Comments;
                    newInput.LastReading = this.selectedMeter.LastReading;
                    newInput.TwoReadingsAgo = this.selectedMeter.TwoReadingsAgo;
                    newInput.ThreeReadingsAgo = this.selectedMeter.ThreeReadingsAgo;
                    newInput.FourReadingsAgo = this.selectedMeter.FourReadingsAgo;
                    this.parent.Input = newInput;
                }

                this.RaisePropertyChanged(nameof(this.Selected));
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadingInputListViewModel"/> class.
        /// </summary>
        public ReadingInputListViewModel(ReadingInputScreenViewModel parent)
        {
            this.Meters = new ObservableCollection<Meter>(/*meter list*/);
            this.parent = parent;
            this.Selected = this.Meters.FirstOrDefault();
        }

        private ReadingInputScreenViewModel parent;
        private Meter selectedMeter;
    }
}
