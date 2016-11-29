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
    public class ReadingInputViewModel : BaseViewModel
    {
        /// <summary>
        /// Gets a value indicating whether if the input type is not.
        /// </summary>
        public bool IsBooleanInput
        {
            get
            {
                return this.data.ValueBounds == BoundType.EitherOr;
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
                return this.data.BooleanValue;
            }

            set
            {
                this.data.BooleanValue = value;
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
                return this.data.StringValue;
            }

            set
            {
                this.data.StringValue = value;
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
                return this.data.Notes;
            }

            set
            {
                this.data.Notes = value;
                this.RaisePropertyChanged(nameof(this.Comments));
            }
        }

        /// <summary>
        /// Gets the accepted value to show on the screen.
        /// </summary>
        public string AcceptedValue
        {
            get
            {
                if (this.data.ValueBounds == BoundType.Between)
                {
                    // must be integer
                    return $"{this.data.MinimumValue} to {this.data.MaximumValue}";
                }

                if (this.data.ValueBounds == BoundType.GreaterThan)
                {
                    return $"> {this.data.MinimumValue}";
                }

                if (this.data.ValueBounds == BoundType.LessThan)
                {
                    return $"< {this.data.MaximumValue}";
                }

                if (this.data.ValueBounds == BoundType.EqualTo)
                {
                    return this.data.ExpectedStringvalue;
                }

                return "N/A";
            }
        }

        /// <summary>
        /// Gets or sets the value to display for the yes value of the
        /// toggle control.
        /// </summary>
        public string YesBooleanText { get; set; }

        /// <summary>
        /// Gets or sets the value to display for the no value of the
        /// toggle control.
        /// </summary>
        public string NoBooleanText { get; set; }

        /// <summary>
        /// Gets the value indicating what type of bounds the input supports.
        /// </summary>
        public BoundType InputType
        {
            get
            {
                return this.data.ValueBounds;
            }
        }

        /// <summary>
        /// Gets the value that was submitted from yesterday.
        /// </summary>
        public string YesterdaysValue
        {
            get
            {
                return this.yesterdaysData.StringValue;
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
                return this.data.IsWithinSpec;
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
        /// Initializes a new instance of the <see cref="ReadingInputViewModel"/> class.
        /// </summary>
        public ReadingInputViewModel()
        {
            this.data = new ReadingInput()
            {
                ValueBounds = BoundType.Between,
                BooleanValue = true,
                StringValue = "Hello",
                MaximumValue = 100,
                MinimumValue = 50,
                ExpectedStringvalue = "123",
                IsWithinSpec = false,
                Notes = "I have some notes!"
            };

            this.yesterdaysData = new ReadingInput()
            {
                Notes = "Did some stuff.",
                StringValue = "25",
                IsWithinSpec = false
            };

            this.YesBooleanText = "True Fact";
            this.NoBooleanText = "False Fact";
        }

        private ReadingInput data;
        private ReadingInput yesterdaysData;
    }
}
