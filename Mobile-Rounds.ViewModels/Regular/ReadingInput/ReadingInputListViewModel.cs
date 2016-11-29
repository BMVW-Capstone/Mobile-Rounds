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
                    ReadingInput yesterday = this.selectedMeter.YesterdaysReading;

                    this.parent.Input = new ReadingInputViewModel(today, yesterday);
                }

                this.RaisePropertyChanged(nameof(this.Selected));
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadingInputListViewModel"/> class.
        /// </summary>
        public ReadingInputListViewModel(ReadingInputScreenViewModel parent)
        {
            if (MockMeters.Count == 0)
            {
                Meter mock1 = new Meter(Guid.NewGuid(), "Supply Air Receiver Tank")
                {
                    TodaysReading = new ReadingInput
                    {
                        IsWithinSpec = true,
                        ValueBounds = Shared.ReadingType.BoundType.EitherOr,
                        YesBooleanText = "Open",
                        NoBooleanText = "Closed",
                    },
                    YesterdaysReading = new ReadingInput()
                    {
                        IsWithinSpec = true,
                        StringValue = "Open"
                    }
                };
                Meter mock2 = new Meter(Guid.NewGuid(), "Nitrogen Backup Valve")
                {
                    TodaysReading = new ReadingInput
                    {
                        IsWithinSpec = true,
                        ValueBounds = Shared.ReadingType.BoundType.LessThan,
                        MinimumValue = 0,
                        MaximumValue = 76,
                        UnitAbbreviation = "K"
                    },
                    YesterdaysReading = new ReadingInput
                    {
                        IsWithinSpec = false,
                        ValueBounds = Shared.ReadingType.BoundType.LessThan,
                        Notes = "Wasn't working at all.",
                        StringValue = "89",
                        UnitAbbreviation = "K"
                    }
                };
                Meter mock3 = new Meter(Guid.NewGuid(), "Nitrogen Backup Pressure")
                {
                    TodaysReading = new ReadingInput
                    {
                        IsWithinSpec = true,
                        ValueBounds = Shared.ReadingType.BoundType.Between,
                        MinimumValue = 55,
                        MaximumValue = 67,
                        UnitAbbreviation = "PSI"
                    },
                    YesterdaysReading = new ReadingInput
                    {
                        IsWithinSpec = true,
                        ValueBounds = Shared.ReadingType.BoundType.Between,
                        StringValue = "60",
                        UnitAbbreviation = "PSI"
                    }
                };
                MockMeters.Add(mock1);
                MockMeters.Add(mock2);
                MockMeters.Add(mock3);
            }

            this.Meters = new ObservableCollection<Meter>(MockMeters);
            this.parent = parent;
        }

        private ReadingInputScreenViewModel parent;
        private Meter selectedMeter;
    }
}
