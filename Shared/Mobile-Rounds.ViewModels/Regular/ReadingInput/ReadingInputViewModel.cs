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
using Mobile_Rounds.ViewModels.Admin.Items;
using Mobile_Rounds.ViewModels.Models;
using Mobile_Rounds.ViewModels.Shared.Commands;

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
                this.todaysData.IsWithinSpec = this.Validate();
                this.RaisePropertyChanged(nameof(this.BooleanValue));
                this.RaisePropertyChanged(nameof(this.IsInSpec));
                this.Save.RaiseExecuteChanged();
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
                this.todaysData.IsWithinSpec = this.Validate();
                this.RaisePropertyChanged(nameof(this.IsInSpec));
                this.RaisePropertyChanged(nameof(this.StringValue));
                this.Save.RaiseExecuteChanged();
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
                this.Save.RaiseExecuteChanged();
            }
        }

        /// <summary>
        /// Gets or sets the string value from the screen.
        /// </summary>
        public string LastComments
        {
            get
            {
                return this.LastReading?.Notes;
            }

            set
            {
                this.LastReading.Notes = value;
                this.RaisePropertyChanged(nameof(this.LastComments));
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
                    return "Parameter";
                }

                return $"Parameter ({this.todaysData.UnitAbbreviation})";
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
                    return $"> {this.todaysData.MaximumValue} {this.todaysData.UnitAbbreviation}";
                }
                if (this.todaysData.ValueBounds == BoundType.GreaterThanOrEqual)
                {
                    return $">= {this.todaysData.MaximumValue} {this.todaysData.UnitAbbreviation}";
                }

                if (this.todaysData.ValueBounds == BoundType.LessThan)
                {
                    return $"< {this.todaysData.MaximumValue} {this.todaysData.UnitAbbreviation}";
                }
                if (this.todaysData.ValueBounds == BoundType.LessThanOrEqual)
                {
                    return $"<= {this.todaysData.MaximumValue} {this.todaysData.UnitAbbreviation}";
                }


                if (this.todaysData.ValueBounds == BoundType.EqualTo)
                {
                    return $"{this.todaysData.MaximumValue} {this.todaysData.UnitAbbreviation}";
                }

                if (this.todaysData.ValueBounds == BoundType.EitherOr)
                {
                    return this.todaysData.MaximumValue;
                }

                return "N/A";
            }
        }

        /// <summary>
        /// Gets the value to display for the yes value of the
        /// toggle control.
        /// </summary>
        public string MaximumValue
        {
            get
            {
                return this.todaysData.MaximumValue;
            }
        }

        /// <summary>
        /// Gets the value to display for the no value of the
        /// toggle control.
        /// </summary>
        public string MinimumValue
        {
            get
            {
                return this.todaysData.MinimumValue;
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
        /// Gets the value indicating if the value was within spec or not.
        /// </summary>
        public bool IsInSpec
        {
            get
            {
                return this.todaysData.IsWithinSpec;
            }

            set
            {
                this.todaysData.IsWithinSpec = value;
                this.RaisePropertyChanged(nameof(this.IsInSpec));
            }
        }

        public Guid ItemId { get; set; }

        /// <summary>
        /// Gets or sets yesterdays reading.
        /// </summary>
        public ReadingInput LastReading { get; set; }

        /// <summary>
        /// Gets or sets yesterdays reading.
        /// </summary>
        public ReadingInput TwoReadingsAgo { get; set; }


        /// <summary>
        /// Gets or sets yesterdays reading.
        /// </summary>
        public ReadingInput ThreeReadingsAgo { get; set; }


        /// <summary>
        /// Gets or sets yesterdays reading.
        /// </summary>
        public ReadingInput FourReadingsAgo { get; set; }

        public ComparisonTypeViewModel ComparisonType { get; set; }

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
        public ReadingInputViewModel(AsyncCommand save, AsyncCommand cancel)
        {
            this.todaysData = new ReadingInput();
            this.ShowInput = false;
            this.Save = save;
            this.Cancel = cancel;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadingInputViewModel"/> class.
        /// </summary>
        public ReadingInputViewModel(ReadingInput todaysData, ItemModel item, AsyncCommand save, AsyncCommand cancel)
            : this(save, cancel)
        {
            this.item = item;
            this.ComparisonType = ComparisonTypeViewModel.Locate(this.item.Specification.ComparisonType);
            this.todaysData = todaysData;
            this.ShowInput = this.todaysData != null;
        }

        public AsyncCommand Save { get; private set; }
        public AsyncCommand Cancel { get; private set; }

        public bool Validate()
        {
            var validator = this.ComparisonType;
            if (validator == null) return false;

            var valid = false;
            if (validator.UsesOneInput)
            {
                valid = validator.Validate(
                    value: this.StringValue,
                    max: this.item.Specification.UpperBound);
            }
            else
            {
                var valToTest = this.IsBooleanInput ? this.BooleanValue.ToString() : this.StringValue;

                if (this.IsBooleanInput)
                {
                    valToTest = this.BooleanValue ? TrueValue : FalseValue;
                }

                valid = validator.Validate(
                        value: valToTest,
                        min: item.Specification.LowerBound,
                        max: item.Specification.UpperBound);

                if(valid && this.IsBooleanInput)
                {
                    //expect the upper bound to be the actual value we want.
                    valid = valToTest.ToLower().Equals(this.item.Specification.UpperBound, StringComparison.CurrentCultureIgnoreCase);
                }
            }

            return valid;
        }



        private string FalseValue => item.Specification.LowerBound;
        private string TrueValue => item.Specification.UpperBound;

        private ReadingInput todaysData;
        private ItemModel item;
        private bool shouldShowInput;
    }
}
