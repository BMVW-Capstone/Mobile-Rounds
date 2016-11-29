// <copyright file="ReadingInput.cs" company="SolarWorld Capstone Team">
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
    public class ReadingInput : NotificationBase
    {
        /// <summary>
        /// Gets or sets the type of binding that the value
        /// is constrained to.
        /// </summary>
        public BoundType ValueBounds { get; set; }

        /// <summary>
        /// Gets or sets the notes for the given reading.
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// Gets or sets a value that is iterpreted as a boolean.
        /// </summary>
        public bool BooleanValue { get; set; }

        /// <summary>
        /// Gets or sets a value that is interpreted as a string.
        /// </summary>
        public string StringValue { get; set; }

        /// <summary>
        /// Gets or sets a value that is the lowest acceptable value.
        /// </summary>
        public int MinimumValue { get; set; }

        /// <summary>
        /// Gets or sets a value that is the maximum acceptable value.
        /// </summary>
        public int MaximumValue { get; set; }

        /// <summary>
        /// Gets or sets a value indicating what the expected
        /// string value should be. This is really only used
        /// in the case of Exact values.
        /// </summary>
        public string ExpectedStringValue { get; set; }

        /// <summary>
        /// Gets or sets the value indicating if the item was within specified spec ranges or not.
        /// </summary>
        public bool IsWithinSpec { get; set; }

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
        /// Gets or sets the value to display for the unit type.
        /// </summary>
        public string UnitAbbreviation { get; set; }
    }
}
