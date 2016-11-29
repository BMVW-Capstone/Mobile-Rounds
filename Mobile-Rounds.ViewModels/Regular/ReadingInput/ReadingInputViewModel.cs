// <copyright file="ReadingInputViewModel.cs" company="SolarWorld Capstone Team">
// Copyright (c) SolarWorld Capstone Team. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mobile_Rounds.ViewModels.Shared;
using Mobile_Rounds.ViewModels.Shared.ReadingType;

namespace Mobile_Rounds.ViewModels.Regular.ReadingInput
{
    /// <summary>
    /// Represents the input fields on the reading screen.
    /// </summary>
    public class ReadingInputViewModel : BaseViewModel
    {
        /// <summary>
        /// Gets a value indicating whether if the input type is not.
        /// </summary>
        public bool IsBooleanInput
        {
            get
            {
                return this.todaysData.ValueBounds == BoundType.EitherOr;
            }
        }

        /// <summary>
        /// Gets a value indicating whether if the input type is not boolean.
        /// </summary>
        public bool IsNotBooleanInput
        {
            get
            {
                return !this.IsBooleanInput;
            }
        }

        /// <summary>
        /// Gets or sets the boolean value from the screen.
        /// </summary>
        public bool BooleanValue
        {
            get
            {
                return this.todaysData.BooleanValue;
            }

            set
            {
                this.todaysData.BooleanValue = value;
                this.RaisePropertyChanged(nameof(this.BooleanValue));
            }
        }

        /// <summary>
        /// Gets or sets the string value from the screen.
        /// </summary>
        public string StringValue
        {
            get
            {
                return this.todaysData.StringValue;
            }

            set
            {
                this.todaysData.StringValue = value;
                this.RaisePropertyChanged(nameof(this.StringValue));
            }
        }

        /// <summary>
        /// Gets or sets the string value from the screen.
        /// </summary>
        public string Comments
        {
            get
            {
                return this.todaysData.Notes;
            }

            set
            {
                this.todaysData.Notes = value;
                this.RaisePropertyChanged(nameof(this.Comments));
            }
        }

        /// <summary>
        /// Gets the header text for the reading field.
        /// </summary>
        public string InputHeader
        {
            get
            {
                if (string.IsNullOrEmpty(this.todaysData.UnitAbbreviation))
                {
                    return "Current";
                }

                return $"Current ({this.todaysData.UnitAbbreviation})";
            }
        }

        /// <summary>
        /// Gets the accepted value to show on the screen.
        /// </summary>
        public string AcceptedValue
        {
            get
            {
                if (this.todaysData.ValueBounds == BoundType.Between)
                {
                    // must be integer
                    return $"{this.todaysData.MinimumValue} to {this.todaysData.MaximumValue} {this.todaysData.UnitAbbreviation}";
                }

                if (this.todaysData.ValueBounds == BoundType.GreaterThan)
                {
                    return $"> {this.todaysData.MinimumValue} {this.todaysData.UnitAbbreviation}";
                }

                if (this.todaysData.ValueBounds == BoundType.LessThan)
                {
                    return $"< {this.todaysData.MaximumValue} {this.todaysData.UnitAbbreviation}";
                }

                if (this.todaysData.ValueBounds == BoundType.EqualTo)
                {
                    return $"{this.todaysData.ExpectedStringValue} {this.todaysData.UnitAbbreviation}";
                }

                if (this.todaysData.ValueBounds == BoundType.EitherOr)
                {
                    return this.todaysData.YesBooleanText;
                }

                return "N/A";
            }
        }

        /// <summary>
        /// Gets the value to display for the yes value of the
        /// toggle control.
        /// </summary>
        public string YesBooleanText
        {
            get
            {
                return this.todaysData.YesBooleanText;
            }
        }

        /// <summary>
        /// Gets the value to display for the no value of the
        /// toggle control.
        /// </summary>
        public string NoBooleanText
        {
            get
            {
                return this.todaysData.NoBooleanText;
            }
        }

        /// <summary>
        /// Gets the value indicating what type of bounds the input supports.
        /// </summary>
        public BoundType InputType
        {
            get
            {
                return this.todaysData.ValueBounds;
            }
        }

        /// <summary>
        /// Gets the value that was submitted from yesterday.
        /// </summary>
        public string YesterdaysValue
        {
            get
            {
                return $"{this.yesterdaysData.StringValue} {this.yesterdaysData.UnitAbbreviation}";
            }
        }

        /// <summary>
        /// Gets the comments from yesterday.
        /// </summary>
        public string YesterdaysComments
        {
            get
            {
                return this.yesterdaysData.Notes;
            }
        }

        /// <summary>
        /// Gets the value indicating if the value was within spec or not.
        /// </summary>
        public bool IsInSpec
        {
            get
            {
                return this.todaysData.IsWithinSpec;
            }
        }

        /// <summary>
        /// Gets the value indicating if the value was within spec or not.
        /// </summary>
        public bool WasYesterdayInSpec
        {
            get
            {
                return this.yesterdaysData.IsWithinSpec;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating if the input screen should be visible.
        /// </summary>
        public bool ShowInput
        {
            get
            {
                return this.shouldShowInput;
            }

            set
            {
                this.shouldShowInput = value;
                this.RaisePropertyChanged(nameof(this.ShowInput));
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadingInputViewModel"/> class.
        /// </summary>
        public ReadingInputViewModel()
        {
            this.todaysData = this.yesterdaysData = new ReadingInput();
            this.ShowInput = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadingInputViewModel"/> class.
        /// </summary>
        public ReadingInputViewModel(ReadingInput todaysData, ReadingInput yesterdaysData)
        {
            this.todaysData = todaysData;
            this.ShowInput = this.todaysData != null;
            this.yesterdaysData = yesterdaysData;
        }

        private ReadingInput todaysData;
        private ReadingInput yesterdaysData;
        private bool shouldShowInput;
    }
}
